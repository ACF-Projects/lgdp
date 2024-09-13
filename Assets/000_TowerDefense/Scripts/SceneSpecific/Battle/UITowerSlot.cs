using RushHour.Data;
using RushHour;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RushHour.Tower;
using RushHour.Tower.Components;

namespace RushHour.UserInterface
{
    public class UITowerSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        [Header("Object Assignments")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private TextMeshProUGUI _costText;
        [Header("Audio Assignments")]
        [SerializeField] private AudioClip _placedUnitSFX;

        private bool _isDraggable = false;  // True if player can pay for this unit, else False
        private bool _isPlaceable = false;  // True if hovering over placeable area, else False
        private TowerData _cachedData;  // Set by other script when initialized

        private TowerHandler _spawnedTowerHandler = null;  // World object tower spawned when dragging

        public void Initialize(TowerData data)
        {
            _costText.text = "$" + data.Cost.ToString();
            _slotImage.sprite = data.UIIcon;
            _cachedData = data;
            DisableDragIfTooPoor();
        }

        public void OnEnable()
        {
            Globals.OnMoneyChanged += DisableDragIfTooPoor;
        }

        public void OnDisable()
        {
            Globals.OnMoneyChanged -= DisableDragIfTooPoor;
        }

        /// <summary>
        /// Only allow the player to drag if the player has enough money
        /// to place the unit this slot represents.
        /// </summary>
        public void DisableDragIfTooPoor()
        {
            _isDraggable = Globals.Money >= _cachedData.Cost;
            _costText.color = Globals.Money >= _cachedData.Cost ? Color.black : Color.red;
        }

        /// <summary>
        /// When we begin a drag, create a world space tower object and cache it.
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isDraggable) { return; }

            ContextManager.instance.ChangeContext(ContextType.None);  // Hide menu when dragging

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            // Create world object, disable its functionality, and bring it to the mouse
            TowerHandler obj = Instantiate(_cachedData.towerPrefab, worldPosition, Quaternion.identity).GetComponent<TowerHandler>();  // Spawn tower
            obj.Init(_cachedData);

            //obj.ShowEffectRadius();
            //obj.IsActivated = false;
            //_spawnedTowerHandler = obj;
        }

        /// <summary>
        /// When we are dragging, make the tower follow the mouse and update its
        /// visuals based on placeability.
        /// </summary>
        //public void Update()
        //{
        //    if (_spawnedTowerHandler != null)
        //    {
        //        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        worldPosition.z = 0;

        //        _spawnedTowerHandler.transform.position = worldPosition;  // Tower follows mouse

        //        // Collision check with blocked objects or UI; check for 1 collision (with itself)
        //        Collider2D[] col = Physics2D.OverlapCircleAll(worldPosition, _cachedData.SpriteRadius, LayerMask.GetMask("Blocked", "Obstacle"));
        //        _isPlaceable = col.Length == 1 && EventSystem.current.currentSelectedGameObject == null;

        //        if (_isPlaceable)
        //        {
        //            _spawnedTowerHandler.TintEffectRadius(Color.white);
        //        }
        //        else
        //        {
        //            _spawnedTowerHandler.TintEffectRadius(Color.red);
        //        }
        //    }
        //}

        /// <summary>
        /// When a drag end, attempt to place currently selected tower at the mouse.
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            if (_spawnedTowerHandler == null) { return; }

            ContextManager.instance.ChangeContext(ContextType.Shop);  // Re-show menu after dragging

            // If we are hovering over an invalid spot, then we cannot place
            if (!_isPlaceable)
            {
                Destroy(_spawnedTowerHandler.gameObject);
                _spawnedTowerHandler = null;
                AudioManager.Instance.PlayOneShot(SoundEffect.InvalidPlacement);
                return;
            }

            // If we don't have sufficient funds, then we also cannot place
            if (Globals.Money < _cachedData.Cost)
            {
                Destroy(_spawnedTowerHandler.gameObject);
                _spawnedTowerHandler = null;
                AudioManager.Instance.PlayOneShot(SoundEffect.InsufficientFunds);
                return;
            }

            _spawnedTowerHandler.IsActivated = true;  // Activate the tower after its placed
            _spawnedTowerHandler = null;

            Globals.Money -= _cachedData.Cost;  // Decrease current funds
            Globals.SalaryPerHour += _cachedData.SalaryPerHour;

            AudioManager.Instance.PlayOneShot(_placedUnitSFX, 0.6f);  // Play sound effect when placed

            // If the unit has a unique placement sound effect, also play that too
            if (_cachedData.PlacementSFX != null)
            {
                AudioManager.Instance.PlayOneShot(_cachedData.PlacementSFX, _cachedData.PlacementSFXVolume);
            }
        }

    }
}
