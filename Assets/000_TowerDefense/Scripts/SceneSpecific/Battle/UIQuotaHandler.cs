using System.Collections;
using System.Collections.Generic;
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
            // BattleManager.Instance.OnMoneyChanged += UpdateQuota;
        }

        private void OnDisable()
        {
            // BattleManager.Instance.OnMoneyChanged -= UpdateQuota;
        }

        private void UpdateQuota()
        {
            // _quotaText.text = _quotaFormatString.Replace("%d", BattleManager.Instance.Money.ToString());
        }

    }
}
