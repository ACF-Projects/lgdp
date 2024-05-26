using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public static class Globals
    {

        private static int _money = 0;
        public static int Money {
            get => _money;
            set
            {
                _money = value;
                OnMoneyChanged?.Invoke();
            }
        }

        public static Action OnMoneyChanged = null;

    }
}
