using NaughtyAttributes;
using RushHour.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RushHour
{
    public class LevelSelectManager : MonoBehaviour
    {
        
        /// <summary>
        /// Contains all parent objects for each section containing different levels.
        /// Order matters when scrolling; first area is shown initially.
        /// </summary>
        [SerializeField, ReorderableList] private List<LevelAreaSection> _areas = new();

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _areaNameText;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;

        /// <summary>
        /// Current index of the area from `_areas` that we are displaying.
        /// 
        /// When updated, automatically calls `ActivateArea()` on the index and updates
        /// button clickability if buttons are assigned.
        /// </summary>
        public int CurrAreaIdx 
        {
            get => _currAreaIdx;
            set
            {
                _currAreaIdx = value;
                ActivateArea(value);

                if (_prevButton != null) { _prevButton.interactable = _currAreaIdx != 0; }
                if (_nextButton != null) { _nextButton.interactable = _currAreaIdx != _areas.Count - 1; }
            }
        }
        private int _currAreaIdx;

        private void Awake()
        {
            CurrAreaIdx = 0;  // Automatically calls `ActivateArea()` through setter method
        }

        /// <summary>
        /// Given the index of the area we want to activate, makes the GameObject for
        /// the selected section show whilst hiding the rest.
        /// 
        /// Also sets the current area's text to be the chosen area.
        /// </summary>
        public void ActivateArea(int idx)
        {
            for (int i = 0; i < _areas.Count; i++)
            {
                _areas[i].gameObject.SetActive(idx == i);
            }

            if (_areaNameText != null)
            {
                _areaNameText.text = _areas[idx].AreaName;
            }
        }

        public void OnClick_PrevArea() => CurrAreaIdx = Mathf.Max(0, CurrAreaIdx - 1);
        public void OnClick_NextArea() => CurrAreaIdx = Mathf.Min(CurrAreaIdx + 1, _areas.Count - 1);

#if UNITY_EDITOR
        /// <summary>
        /// Add all `LevelAreaSection` objects in scene to this object's known areas.
        /// 
        /// Only adds sections that were newly found to not mess up previously created orders.
        /// </summary>
        protected virtual void OnValidate()
        {
            List<LevelAreaSection> sections = new(FindObjectsOfType<LevelAreaSection>(true));
            sections = sections.FindAll((sec) => !_areas.Contains(sec)); 
            foreach (LevelAreaSection section in sections)
            {
                _areas.Add(section);
            }
        }
#endif

    }
}
