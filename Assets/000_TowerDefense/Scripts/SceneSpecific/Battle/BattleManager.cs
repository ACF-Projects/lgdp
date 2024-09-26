using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RushHour.Global;

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
        private float _earnedMoney = 0;
        public float Money
        {
            get => _money;
            private set
            {
                _money = value;
                OnMoneyChanged?.Invoke();
            }
        }
        public float EarnedMoney => _earnedMoney;

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

        [Header("Battle Properties")]
        [SerializeField] private int _requiredQuota = 100;  // How much is needed to win level
        public int RequiredQuota => _requiredQuota;

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

        /// <summary>
        /// Whenever the player earns money (e.g., acquires a customer), tracks the
        /// earned money for the quota and updates total money. Earned money only increases if the change amount is positive.
        /// </summary>
        public void ChangeMoney(float changeAmount)
        {
            Money += changeAmount;
            if(changeAmount > 0) _earnedMoney += changeAmount;
        }

        /// <summary>
        /// Changes the salary by the specified amount.
        /// </summary>
        /// <param name="changeAmount"></param>
        public void ChangeSalary(float changeAmount)
        {
            SalaryPerHour += changeAmount;
        }
    }
}
