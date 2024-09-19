using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class BattleManager : MonoBehaviour
    {

        private static BattleManager _instance;
        public static BattleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<BattleManager>();
                }
                return _instance;
            }
        }

        private float _money = 500;
        public float Money
        {
            get => _money;
            set
            {
                _money = value;
                OnMoneyChanged?.Invoke();
            }
        }

        private float _salaryPerHour = 0;
        public float SalaryPerHour
        {
            get => _salaryPerHour;
            set
            {
                _salaryPerHour = value;
                OnSalaryChanged?.Invoke();
            }
        }

        private int _timer;
        private int _lastTimeTracked = 0;  // Updated when an hour is processed
        public int Timer
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

        public static int TIME_IN_DAY = 60;  // Number of seconds in a day until day ends

        public Action OnMoneyChanged = null;
        public Action OnSalaryChanged = null;

        public Action<int> OnTimerChanged = null;  // Param is current time
        public Action OnNewHour = null;  // Called whenever 60 minutes in-game has passed

        private void Awake()
        {
            if (!ReferenceEquals(_instance, this))
            {
                if (_instance != null)
                {
                    Destroy(this);
                }
                else
                {
                    _instance = this;
                }
            }
        }

        private void OnEnable()
        {
            OnNewHour += ChargeSalary;
        }

        private void OnDisable()
        {
            OnNewHour -= ChargeSalary;
        }

        /// <summary>
        /// Decrements the player's current money by the salary amount per hour.
        /// </summary>
        private void ChargeSalary()
        {
            Money -= SalaryPerHour;
        }

    }
}
