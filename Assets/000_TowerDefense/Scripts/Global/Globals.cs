using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public static class Globals
    {

        private static float _money = 500;
        public static float Money {
            get => _money;
            set
            {
                _money = value;
                OnMoneyChanged?.Invoke();
            }
        }

        private static float _salaryPerHour = 0;
        public static float SalaryPerHour
        {
            get => _salaryPerHour;
            set
            {
                _salaryPerHour = value;
                OnSalaryChanged?.Invoke();
            }
        }

        private static int _timer;
        private static int _lastTimeTracked = 0;  // Updated when an hour is processed
        public static int Timer
        {
            get => _timer;
            set
            {
                _timer = value;
                OnTimerChanged?.Invoke(value);

                // If many hours have passed, call OnNewHour() for each hour passed
                int hoursPassed = (_timer - _lastTimeTracked) / 60;
                if (hoursPassed > 0)
                {
                    _lastTimeTracked = _timer;
                }
                for (int i = 0; i < hoursPassed; i++)
                {
                    OnNewHour?.Invoke();
                }
            }
        }

        public static Action OnMoneyChanged = null;
        public static Action OnSalaryChanged = null;

        public static Action<int> OnTimerChanged = null;  // Param is current time
        public static Action OnNewHour = ChargeSalary;  // Called whenever 60 minutes in-game has passed

        /// <summary>
        /// Decrements the player's current money by the salary amount per hour.
        /// </summary>
        private static void ChargeSalary() 
        {
            Money -= SalaryPerHour;
        }

    }
}
