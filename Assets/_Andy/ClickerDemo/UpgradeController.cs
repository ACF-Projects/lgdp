using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.Game.Upgrades
{
    public enum UpgradeType
    {
        MoneyPerClick,
        AutoClicker,
    }

    [System.Serializable]
    public class ShopUpgrade
    {
        public UpgradeType type;
        public int cost;
    }

    public class UpgradeController : MonoBehaviour
    {
        public void BuyUpgrade(int type)
        {
            var upgrade = GameController.instance.upgrades[type];
            if (GameController.instance.money < upgrade.cost) return;
            GameController.instance.AddMoney(-upgrade.cost);
            switch (upgrade.type)
            {
                case UpgradeType.MoneyPerClick:
                    GameController.instance.moneyPerClick++;
                    break;
                case UpgradeType.AutoClicker:
                    GameController.instance.autoClicksPerSecond++;
                    break;
            }
        }
    }
}
