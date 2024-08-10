using LGDP.TowerDefense.Data;
using RushHour;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LGDP.TowerDefense
{
    [RequireComponent(typeof(UIDraggable))]
    public class UITowerSlot : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private Image _slotImage;
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
            _slotImage.GetComponent<RectTransform>().sizeDelta = new Vector2(_cachedData.TowerSprite.rect.width * _cachedData.SpriteScale.x, 
                                                                             _cachedData.TowerSprite.rect.height * _cachedData.SpriteScale.y);
        }

        /// <summary>
        /// When this unit is being dragged around, check if it is colliding with
        /// anything in world space, and edit _isPlaceable correspondingly.
        /// </summary>
        public void UIDraggable_WhileDrag(Vector3 currPosition)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currPosition);

            float towerPlaceRadius = _cachedData.PlacementRadius;
            Collider2D[] col = Physics2D.OverlapCircleAll(worldPosition, towerPlaceRadius, LayerMask.GetMask("Blocked"));
           
            // We are placeable if not colliding with blocked objects or UI objects
            _isPlaceable = col.Length == 0 && EventSystem.current.currentSelectedGameObject == null;
        }

        /// <summary>
        /// Given a position in UI space, attempts to place this
        /// tower at that location. 
        /// </summary>
        public void UIDraggable_TryPlaceAt(Vector3 placedPosition)
        {
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

            Instantiate(_cachedData.towerPrefab, worldPosition, Quaternion.identity);  // Spawn tower
            Globals.Money -= _cachedData.Cost;  // Decrease current funds
        }

    }
}
