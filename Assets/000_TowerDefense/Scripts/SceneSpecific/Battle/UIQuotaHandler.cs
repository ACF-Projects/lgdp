using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace RushHour
{
    public class UIQuotaHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _quotaText;
        [Header("Text Properties")]
        [SerializeField] private string _quotaFormatString;

        private void OnEnable()
        {
            UpdateQuota();
            BattleManager.Instance.OnMoneyChanged += UpdateQuota;
        }

        private void OnDisable()
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnMoneyChanged -= UpdateQuota;
            }
        }

        private void UpdateQuota()
        {
            _quotaText.text = _quotaFormatString.Replace("%earn", BattleManager.Instance.EarnedMoney.ToString())
                                     .Replace("%total", BattleManager.Instance.RequiredQuota.ToString());
        }

    }
}
