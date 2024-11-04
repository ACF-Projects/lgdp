using RushHour.Data;
using RushHour;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RushHour.Tower;

namespace RushHour.UserInterface
{
    public class UITowerSlot : MonoBehaviour, IPointerDownHandler
    {

        [Header("Object Assignments")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private Image _slotBGImage;
        [SerializeField] private TextMeshProUGUI _costText;
        [Header("Audio Assignments")]
        [SerializeField] private AudioClip _placedUnitSFX;

        private bool _isDraggable = false;  // True if player can pay for this unit, else False
        private TowerData _cachedData;  // Set by other script when initialized

        //private TowerHandler _spawnedTowerHandler = null;  // World object tower spawned when dragging

        public void Initialize(TowerData data)
        {
            _costText.text = "<size=24>$" + data.Cost.ToString() + "</size>\n(<color=\"red\">-$" + data.SalaryPerHour.ToString() + "/hr</color>)";
            _slotImage.sprite = data.UIIcon;
            _cachedData = data;
            DisableDragIfTooPoor();
        }

        public void OnEnable()
        {
            BattleManager.Instance.OnMoneyChanged += DisableDragIfTooPoor;
        }

        public void OnDisable()
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnMoneyChanged -= DisableDragIfTooPoor;
            }
        }

        /// <summary>
        /// Only allow the player to drag if the player has enough money
        /// to place the unit this slot represents.
        /// </summary>
        public void DisableDragIfTooPoor()
        {
            _isDraggable = BattleManager.Instance.Money >= _cachedData.Cost;
            _slotBGImage.color = BattleManager.Instance.Money >= _cachedData.Cost ? Color.white : new Color(0.6f, 0.6f, 0.6f);
            _slotImage.color = BattleManager.Instance.Money >= _cachedData.Cost ? Color.white : new Color(0.6f, 0.6f, 0.6f);
            _costText.color = BattleManager.Instance.Money >= _cachedData.Cost ? Color.black : Color.red;
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
        }

    }
}
