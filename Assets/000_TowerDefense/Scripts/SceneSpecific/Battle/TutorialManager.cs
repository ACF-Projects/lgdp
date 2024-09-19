using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class TutorialManager : MonoBehaviour
    {

        [Header("Popups and Tooltips")]
        [SerializeField] private GameObject _welcomePopup;

        private void Start()
        {
            // _welcomePopup.SetActive(true);
        }

        /// <summary>
        /// Register events to show/hide popups at correct times.
        /// </summary>
        private void OnEnable()
        {

        }

        /// <summary>
        /// Unregister all events made in OnEnable().
        /// </summary>
        private void OnDisable()
        {
            
        }

    }
}
