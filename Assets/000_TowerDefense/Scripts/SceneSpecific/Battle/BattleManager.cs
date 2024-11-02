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

        private float _money = 100;
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
                if (value > Constants.TIME_IN_DAY) { return; }  // Stop if over max time

                _timer = Mathf.Min(value, Constants.TIME_IN_DAY);  // Cannot go over max time
                OnTimerChanged?.Invoke(_timer);

                // Check to see if the game is over
                if (_timer == Constants.TIME_IN_DAY)
                {
                    OnDayEnd?.Invoke();
                }

                // If many hours have passed, call OnNewHour() for each hour passed
                int hoursPassed = (_timer - _lastTimeTracked) / Constants.TIME_IN_HOUR;
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

        public int ConvertedCustomers = 0;
        public float TotalSalaryPaid = 0;

        [Header("Battle Properties")]
        [SerializeField] private int _startingMoney = 100;  // How much the player starts with
        [SerializeField] private int _requiredQuota = 100;  // How much is needed to win level
        public int RequiredQuota => _requiredQuota;

        public Action OnMoneyChanged = null;
        public Action OnSalaryChanged = null;

        public Action<int> OnTimerChanged = null;  // Param is current time
        public Action OnNewHour = null;  // Called whenever 60 minutes in-game has passed
        public Action OnDayEnd = null;  // Called when the day is over

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

        private void Start()
        {
            Money = _startingMoney;  // Set starting money
        }

        private void OnEnable()
        {
            OnNewHour += ChargeSalary;
            EnemyHandler.OnStoreReached += EnemyStoreReached;
        }

        private void OnDisable()
        {
            OnNewHour -= ChargeSalary;
            EnemyHandler.OnStoreReached -= EnemyStoreReached;
        }

        private void EnemyStoreReached(EnemyHandler handler)
        {
            ChangeMoney(handler.EnemyData.Reward);
        }

        /// <summary>
        /// Decrements the player's current money by the salary amount per hour.
        /// </summary>
        private void ChargeSalary()
        {
            TotalSalaryPaid += SalaryPerHour;  // Keep track of total salary paid for summary screen
            Money -= SalaryPerHour;
        }

        /// <summary>
        /// Adds to the player's balance.
        /// 
        /// Optional `shouldAffectQuota` parameter to see if money counts towards quota.
        /// </summary>
        public void ChangeMoney(float changeAmount, bool shouldAffectQuota = true)
        {
            if (changeAmount > 0) _earnedMoney += changeAmount;
            Money += changeAmount;
        }

        /// <summary>
        /// Changes the salary by the specified amount.
        /// </summary>
        /// <param name="changeAmount"></param>
        public void ChangeSalary(float changeAmount)
        {
            SalaryPerHour += changeAmount;
        }

        /// <summary>
        /// Stops all game logic and animates the day end scene.
        /// </summary>
        public void DayEnd()
        {
            TransitionManager.Instance.GoToScene("LevelSelect");
        }

    }
}
