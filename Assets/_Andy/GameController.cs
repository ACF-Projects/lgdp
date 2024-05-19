using LGDP.Game.Upgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public int money = 0;
        public int moneyPerClick = 1;
        public int autoClicksPerSecond = 0;

        public event Action<int, int> Money_OnChange;

        public List<ShopUpgrade> upgrades;

        private float autoClickTimer = 1;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        public void AddMoney()
        {
            AddMoney(moneyPerClick);
        }

        public void AddMoney(int amount)
        {
            Money_OnChange?.Invoke(money, money + amount);
            money += amount;
        }

        private void Update()
        {
            if(autoClickTimer > 0)
            {
                autoClickTimer -= Time.deltaTime;
            }
            else
            {
                AddMoney(moneyPerClick * autoClicksPerSecond);
                autoClickTimer = 1;
            }
        }

    }
}
