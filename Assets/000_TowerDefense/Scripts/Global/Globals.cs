using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense
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

        private static int _timer;
        public static int Timer
        {
            get => _timer;
            set
            {
                _timer = value;
                OnTimerChanged?.Invoke(value);
            }
        }

        public static Action OnMoneyChanged = null;
        public static Action<int> OnTimerChanged = null;  // Param is current time

    }
}
