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
    public class UITowerSlot : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private TextMeshProUGUI _costText;

        private bool _isPlaceable = false;  // True if hovering over placeable area, else False
        private TowerData _cachedData;  // Set by other script when initialized

        public void Initialize(TowerData data)
        {
            _costText.text = "$" + data.Cost.ToString();
            _slotImage.sprite = data.Icon;
            _cachedData = data;
        }

        public void OnEnable()
        {
            if (TryGetComponent(out UIDraggable drag))
            {
                drag.OnBeingDragged += WhileDrag;
                drag.OnDropped += TryPlaceAt;
            }
        }

        public void OnDisable()
        {
            if (TryGetComponent(out UIDraggable drag))
            {
                drag.OnBeingDragged += WhileDrag;
                drag.OnDropped -= TryPlaceAt;
            }
        }

        public void WhileDrag(Vector3 currPosition)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currPosition);

            // TODO: The default radius for towers is 0.6f. Make this dynamic by assigning val in ScriptableObject
            Collider2D[] col = Physics2D.OverlapCircleAll(worldPosition, 0.6f, LayerMask.GetMask("Blocked"));
           
            // We are placeable if not colliding with blocked objects or UI objects
            _isPlaceable = col.Length == 0 && EventSystem.current.currentSelectedGameObject == null;
        }

        /// <summary>
        /// Given a position in UI space, attempts to place this
        /// tower at that location. 
        /// </summary>
        public void TryPlaceAt(Vector3 placedPosition)
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
