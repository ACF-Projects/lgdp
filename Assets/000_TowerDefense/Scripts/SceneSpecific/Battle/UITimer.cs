using RushHour.Global;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour.UserInterface
{
    /// <summary>
    /// Updates some fields based on the BattleManager.Instance.timer variable.
    /// </summary>
    public class UITimer : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _timerDayText;
        [SerializeField] private Image _timerFillImage;

        private void OnEnable()
        {
            BattleManager.Instance.OnTimerChanged += UpdateTimerValue;
        }

        private void OnDisable()
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnTimerChanged -= UpdateTimerValue;
            }
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
            _timerDayText.text = CalculateTimeString(timeElapsed, Constants.TIME_IN_DAY);

            _timerFillImage.fillAmount = (float)timeElapsed / Constants.TIME_IN_DAY;
            _timerDayText.text = CalculateTimeString(timeElapsed, Constants.TIME_IN_DAY);
        }

        /// <summary>
        /// Retrieve a string between 9am and 6pm depending on the amount of time
        /// that has elapsed, determined by currTime and maxTime.
        /// </summary>
        public static string CalculateTimeString(int currTime, int maxTime)
        {
            int startTimeMinutes = 9 * 60;  // 9:00am in minutes
            int endTimeMinutes = 18 * 60;   // 6:00pm in minutes
            int totalDuration = endTimeMinutes - startTimeMinutes;

            // Calculate how many of these "hours" have passed
            int elapsedHours = currTime / Constants.TIME_IN_HOUR;
            int elapsedMinutes = (currTime % Constants.TIME_IN_HOUR) * (60 / Constants.TIME_IN_HOUR);

            // Calculate the current time
            int currentTimeMinutes = startTimeMinutes + (elapsedHours * 60) + elapsedMinutes;
            int hours = currentTimeMinutes / 60;
            int minutes = currentTimeMinutes % 60;

            // Determine AM/PM
            string period = hours < 12 ? "AM" : "PM";

            // Adjust hour to 12-hour format
            if (hours > 12)
            {
                hours -= 12;
            }
            else if (hours == 0)
            {
                hours = 12;
            }

            // Format the time string to show only hours
            string timeString = $"{hours}:{minutes:D2} {period}";

            return timeString;
        }

        /// <summary>
        /// Retrieve a time string starting at 7:00 AM, adding one minute for 
        /// every second elapsed.
        /// </summary>
        //public static string CalculateTimeString(int elapsedTimeSeconds)
        //{
        //    // Start time is 7:00 AM
        //    DateTime startTime = new(2024, 8, 23, 7, 0, 0);

        //    // Calculate the new time by adding the elapsed time in minutes
        //    DateTime updatedTime = startTime.AddMinutes(elapsedTimeSeconds);

        //    // Return the updated time as a string in the format "h:mm tt"
        //    return updatedTime.ToString("h:mm tt");
        //}

    }
}
