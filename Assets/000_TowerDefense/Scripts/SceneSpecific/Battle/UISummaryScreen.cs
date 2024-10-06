using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour
{
    public class UISummaryScreen : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private Animator _transitionAnimator;
        [SerializeField] private TextMeshProUGUI _feedbackText;  // Success or Fail text
        [SerializeField] private TextMeshProUGUI _moneyMadeText;
        [SerializeField] private TextMeshProUGUI _customersConvertedText;
        [SerializeField] private TextMeshProUGUI _salarySpentText;

        private void OnEnable()
        {
            BattleManager.Instance.OnDayEnd += ShowSummary;
        }

        private void OnDisable()
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnDayEnd -= ShowSummary;
            }
        }

        /// <summary>
        /// Animate the final summary screen and load any needed data.
        /// </summary>
        private void ShowSummary()
        {
            StartCoroutine(ShowSummaryCoroutine());
        }

        private IEnumerator ShowSummaryCoroutine()
        {
            // First, update text
            bool metQuota = BattleManager.Instance.EarnedMoney >= BattleManager.Instance.RequiredQuota;
            _feedbackText.text = metQuota ? "You met the quota!" : "Better luck next time...";
            _feedbackText.color = metQuota ? Color.green : Color.red;
            _moneyMadeText.text = "$" + BattleManager.Instance.EarnedMoney + " raised!";
            _salarySpentText.text = "$" + BattleManager.Instance.TotalSalaryPaid + " spent on employees.";
            _customersConvertedText.text = BattleManager.Instance.ConvertedCustomers + " customers converted!";

            // Then play animator
            _transitionAnimator.Play("Show");
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => _transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            _transitionAnimator.Play("ClickToContinue");

            // Wait until the user clicks the mouse button to continue
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            TransitionManager.Instance.GoToScene("LevelSelect");
        }

    }
}
