using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour
{
    public class UIMoneyHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void OnEnable()
        {
            UpdateMoney();
            Globals.OnMoneyChanged += UpdateMoney;
        }

        private void OnDisable()
        {
            Globals.OnMoneyChanged -= UpdateMoney;
        }

        private void UpdateMoney()
        {
            _moneyText.text = "Money: $" + Globals.Money.ToString();
        }

    }
}
