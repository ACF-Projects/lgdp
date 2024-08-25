using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour
{
    public class UISalaryHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _salaryText;

        private void OnEnable()
        {
            UpdateSalary();
            Globals.OnSalaryChanged += UpdateSalary;
        }

        private void OnDisable()
        {
            Globals.OnSalaryChanged -= UpdateSalary;
        }

        private void UpdateSalary()
        {
            _salaryText.text = "Salary/hr: $" + Globals.SalaryPerHour.ToString();
        }

    }
}
