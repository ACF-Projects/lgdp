using LGDP.TowerDefense.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class UITowerSlot : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _costText;

        public void Initialize(TowerData data)
        {
            _costText.text = "$" + data.Cost.ToString();
        }

    }
}
