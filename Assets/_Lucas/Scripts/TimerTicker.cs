using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.Lucas.POC
{
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
