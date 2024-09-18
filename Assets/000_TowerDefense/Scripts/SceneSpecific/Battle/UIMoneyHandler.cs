using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour.UserInterface
{
    public class UIMoneyHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _moneyText;
        [Header("Text Properties")]
        [SerializeField] private string _moneyFormatString;

        private void OnEnable()
        {
            UpdateMoney();
            BattleManager.Instance.OnMoneyChanged += UpdateMoney;
        }

        private void OnDisable()
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnMoneyChanged -= UpdateMoney;
            }
        }

        private void UpdateMoney()
        {
            _moneyText.text = _moneyFormatString.Replace("%d", BattleManager.Instance.Money.ToString());
        }

    }
}
