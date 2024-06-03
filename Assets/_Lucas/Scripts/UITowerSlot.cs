using LGDP.TowerDefense.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LGDP.TowerDefense
{
    public class UITowerSlot : MonoBehaviour, IPointerClickHandler
    {

        [Header("Object Assignments")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private TextMeshProUGUI _costText;

        private TowerData _cachedData;  // Set when initialized

        public void Initialize(TowerData data)
        {
            _costText.text = "$" + data.Cost.ToString();
            _slotImage.sprite = data.Icon;
            _cachedData = data;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // If we don't have any funds, ignore
            if (_cachedData.Cost > Globals.Money)
            {
                return;
            }
            // If we're already trying to place a tower, ignore
            if (TowerPlaceHandler.IsPlacingTower)
            {
                return;
            }
            Instantiate(_cachedData.towerPrefab);  // Spawn tower
            Globals.Money -= _cachedData.Cost;  // Decrease current funds
        }

    }
}
