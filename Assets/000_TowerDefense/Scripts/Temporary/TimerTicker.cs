using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    /// <summary>
    /// Each instance of an object with this script will tick
    /// the timer up by one every second.
    /// </summary>
    public class TimerTicker : MonoBehaviour
    {

        private void Start()
        {
            InvokeRepeating(nameof(TickTimer), 1, 1);
            Debug.Log("Timer initialized!");
        }

        /// <summary>
        /// Ticks the timer up by one second.
        /// </summary>
        public void TickTimer()
        {
            Globals.Timer++;
        }

    }
}
