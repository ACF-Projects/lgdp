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
        [SerializeField] private TextMeshProUGUI _customersConvertedText;

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
            _feedbackText.text = "You succeeded";  // TODO: Make this vary
            _customersConvertedText.text = "100 customers converted";

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
