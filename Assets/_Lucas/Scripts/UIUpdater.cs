using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class UIMoneyHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void Awake()
        {
            Globals.OnMoneyChanged += () =>
            {
                _moneyText.text = "Money: $" + Globals.Money.ToString();
            };
        }

        private void Start()
        {
            Globals.OnMoneyChanged.Invoke();
        }

    }
}
