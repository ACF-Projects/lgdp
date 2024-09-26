using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour
{
    public class TutorialManager : MonoBehaviour
    {

        [Header("Popups and Tooltips")]
        [SerializeField] private GameObject _welcomePopup;
        [SerializeField] private Button _welcomePopupButton;

        private void Start()
        {
            _welcomePopup.SetActive(true);
            // Initially disable all timer tickers
            ToggleTimerTickers();
        }

        /// <summary>
        /// Register events to show/hide popups at correct times.
        /// </summary>
        private void OnEnable()
        {
            // When the welcome button is clicked, re-enable timer tickers
            _welcomePopupButton.onClick.AddListener(ToggleTimerTickers);
        }

        /// <summary>
        /// Unregister all events made in OnEnable().
        /// </summary>
        private void OnDisable()
        {
            _welcomePopupButton.onClick.RemoveListener(ToggleTimerTickers);
        }

        /// <summary>
        /// Finds all timer tickers in the scene and toggles them from
        /// active to inactive, or vice versa.
        /// </summary>
        private void ToggleTimerTickers()
        {
            TimerTicker[] timerTickers = FindObjectsOfType<TimerTicker>();
            foreach (TimerTicker timerTicker in timerTickers)
            {
                timerTicker.CanTick = !timerTicker.CanTick;
            }
        }

    }
}
