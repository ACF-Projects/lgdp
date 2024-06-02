using LGDP.TowerDefense.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class UISlotsRenderer : MonoBehaviour
    {

        [Header("Prefab Assignments")]
        [SerializeField] private GameObject _uiTowerSlotPrefab;
        [Header("Object Assignments")]
        [SerializeField] private Transform _slotParentTransform;

        private void Start()
        {
            foreach (TowerData td in TowerStore.Instance.AllTowerData)
            {
                GameObject slot = Instantiate(_uiTowerSlotPrefab, _slotParentTransform);
                slot.GetComponent<UITowerSlot>().Initialize(td);
            }
        }

    }
}
