using LGDP.TowerDefense.Data;
using RushHour;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lucas
{
    [RequireComponent(typeof(UIDraggable))]
    public class UITowerSlot : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private Image _effectPreviewImage;
        [SerializeField] private TextMeshProUGUI _costText;

        private bool _isPlaceable = false;  // True if hovering over placeable area, else False
        private TowerData _cachedData;  // Set by other script when initialized

        private UIDraggable _draggable;

        private void Awake()
        {
            _draggable = GetComponent<UIDraggable>();
        }

        public void Initialize(TowerData data)
        {
            _costText.text = "$" + data.Cost.ToString();
            _slotImage.sprite = data.UIIcon;
            _cachedData = data;
            _effectPreviewImage.sprite = data.EffectPreviewSprite;
            _effectPreviewImage.enabled = false;
        }

        public void OnEnable()
        {
            Globals.OnMoneyChanged += DisableDragIfTooPoor;
            _draggable.OnStartDrag += UIDraggable_BeginDrag;
            _draggable.OnBeingDragged += UIDraggable_WhileDrag;
            _draggable.OnDropped += UIDraggable_TryPlaceAt;
        }

        public void OnDisable()
        {
            Globals.OnMoneyChanged -= DisableDragIfTooPoor;
            _draggable.OnStartDrag -= UIDraggable_BeginDrag;
            _draggable.OnBeingDragged -= UIDraggable_WhileDrag;
            _draggable.OnDropped -= UIDraggable_TryPlaceAt;
        }

        /// <summary>
        /// Only allow the player to drag if the player has enough money
        /// to place the unit this slot represents.
        /// </summary>
        public void DisableDragIfTooPoor()
        {
            _draggable.enabled = Globals.Money >= _cachedData.Cost;
            _costText.color = Globals.Money >= _cachedData.Cost ? Color.black : Color.red;
        }

        /// <summary>
        /// When this unit begins dragging, make sure the scale mimics the
        /// world sprite's size for full accuracy.
        /// </summary>
        public void UIDraggable_BeginDrag(Vector3 startPosition)
        {
            MatchUISpriteToWorldSprite(_cachedData.TowerSprite, _cachedData.SpriteScale, _slotImage.GetComponent<RectTransform>(), Camera.main);

            ContextManager.instance.ChangeContext(ContextType.None);  // Hide menu when dragging

            _effectPreviewImage.enabled = true;
            float spriteDiameter = _cachedData.EffectPreviewSprite.bounds.size.x; // Assuming the sprite is a circle
            float scaleFactor = _cachedData.EffectRadius * 2f / spriteDiameter;
            MatchUISpriteToWorldSprite(_cachedData.EffectPreviewSprite, new Vector2(scaleFactor, scaleFactor), _effectPreviewImage.GetComponent<RectTransform>(), Camera.main);
        }

        #region Code to match UI sprite to world sprite
        /// <summary>
        /// Given a sprite and scale for that sprite, perfectly sizes a passed in UI RectTransform
        /// to appear like that sprite in world position.
        /// </summary>
        public static void MatchUISpriteToWorldSprite(Sprite worldSprite, Vector2 worldSpriteScale, RectTransform uiElement, Camera mainCamera)
        {
            // Calculate the world sprite's size in world units
            Vector2 worldSpriteSizeInWorldUnits = new(
                worldSprite.bounds.size.x * worldSpriteScale.x,
                worldSprite.bounds.size.y * worldSpriteScale.y
            );

            // Convert world units to screen pixels
            Vector3 worldSpriteSizeInScreenSpace = mainCamera.WorldToScreenPoint(new(worldSpriteSizeInWorldUnits.x, worldSpriteSizeInWorldUnits.y, 0))
                                                   - mainCamera.WorldToScreenPoint(Vector3.zero);

            float worldSpriteWidthInPixels = Mathf.Abs(worldSpriteSizeInScreenSpace.x);
            float worldSpriteHeightInPixels = Mathf.Abs(worldSpriteSizeInScreenSpace.y);

            // Adjust the UI element's size to match the world sprite's pixel size
            uiElement.sizeDelta = new Vector2(worldSpriteWidthInPixels, worldSpriteHeightInPixels);
        }
        #endregion

        /// <summary>
        /// When this unit is being dragged around, check if it is colliding with
        /// anything in world space, and edit _isPlaceable correspondingly.
        /// </summary>
        public void UIDraggable_WhileDrag(Vector3 currPosition)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currPosition);

            float towerPlaceRadius = _cachedData.SpriteRadius;
            Collider2D[] col = Physics2D.OverlapCircleAll(worldPosition, towerPlaceRadius, LayerMask.GetMask("Blocked", "Obstacle"));

            // We are placeable if not colliding with blocked objects or UI objects
            _isPlaceable = col.Length == 0 && EventSystem.current.currentSelectedGameObject == null;
        }

        /// <summary>
        /// Given a position in UI space, attempts to place this
        /// tower at that location. 
        /// </summary>
        public void UIDraggable_TryPlaceAt(Vector3 placedPosition)
        {
            _effectPreviewImage.enabled = false;

            ContextManager.instance.ChangeContext(ContextType.Shop);  // Re-show menu after dragging

            // If we are hovering over an invalid spot, then we cannot place
            if (!_isPlaceable)
            {
                return;
            }

            // If we don't have sufficient funds, then we also cannot place
            if (Globals.Money < _cachedData.Cost)
            {
                return;
            }

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(placedPosition);
            worldPosition.z = 0;

            GameObject obj = Instantiate(_cachedData.towerPrefab, worldPosition, Quaternion.identity);  // Spawn tower
            obj.GetComponent<TowerHandler>().Init(_cachedData);
            Globals.Money -= _cachedData.Cost;  // Decrease current funds
        }

    }
}
