using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour.UserInterface
{
    public class UISalaryHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _salaryText;
        [Header("Text Properties")]
        [SerializeField] private string _moneyFormatString;

        private void OnEnable()
        {
            UpdateSalary();
            BattleManager.Instance.OnSalaryChanged += UpdateSalary;
        }

        private void OnDisable()
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnSalaryChanged -= UpdateSalary;
            }
        }

        private void UpdateSalary()
        {
            _salaryText.text = _moneyFormatString.Replace("%d", BattleManager.Instance.SalaryPerHour.ToString());
        }

    }
}
