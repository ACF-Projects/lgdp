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

        private void Awake()
        {
            Time.timeScale = 0;
            _welcomePopup.SetActive(true);
        }

        /// <summary>
        /// Register events to show/hide popups at correct times.
        /// </summary>
        private void OnEnable()
        {
            _welcomePopupButton.onClick.AddListener(ToggleTimeScale);
        }

        /// <summary>
        /// Unregister all events made in OnEnable().
        /// </summary>
        private void OnDisable()
        {
            _welcomePopupButton.onClick.RemoveListener(ToggleTimeScale);
        }

        private void ToggleTimeScale()
        {
            Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        }

    }
}
