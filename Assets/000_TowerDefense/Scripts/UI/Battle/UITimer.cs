using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

namespace RushHour
{
    /// <summary>
    /// Updates some fields based on the Globals timer variable.
    /// </summary>
    public class UITimer : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _timerDayText;

        private void OnEnable()
        {
            Globals.OnTimerChanged += UpdateTimerValue;
        }

        private void OnDisable()
        {
            Globals.OnTimerChanged -= UpdateTimerValue;
        }

        private void Start()
        {
            UpdateTimerValue(0);  // Start with zero seconds
        }

        /// <summary>
        /// Given an amount of time that has passed and the total amount of time,
        /// updates the UI to show that time.
        /// </summary>
        private void UpdateTimerValue(int timeElapsed)
        {
            _timerDayText.text = CalculateTimeString(timeElapsed);
        }

        /// <summary>
        /// Retrieve a time string starting at 7:00 AM, adding one minute for 
        /// every second elapsed.
        /// </summary>
        public static string CalculateTimeString(int elapsedTimeSeconds)
        {
            // Start time is 7:00 AM
            DateTime startTime = new(2024, 8, 23, 7, 0, 0);

            // Calculate the new time by adding the elapsed time in minutes
            DateTime updatedTime = startTime.AddMinutes(elapsedTimeSeconds);

            // Return the updated time as a string in the format "h:mm tt"
            return updatedTime.ToString("h:mm tt");
        }

    }
}
