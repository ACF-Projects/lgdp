using LGDP.TowerDefense.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class UISlotManager : MonoBehaviour
    {

        [Header("Prefab Assignments")]
        public GameObject UITowerSlotPrefab;

        private void Start()
        {
            foreach (TowerData td in TowerStore.Instance.AllTowerData)
            {
                GameObject slot = Instantiate(UITowerSlotPrefab);
            }
        }

    }
}
