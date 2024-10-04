using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower.Components
{
    public class TowerMoney : TowerComponent
    {
        public override void Init(TowerHandler handler)
        {
            base.Init(handler);

            TowerMove.OnTowerBought += TowerMove_OnTowerBought;
        }

        private void OnDestroy()
        {
            TowerMove.OnTowerBought-= TowerMove_OnTowerBought;
        }

        private void TowerMove_OnTowerBought(object sender, System.EventArgs e)
        {
            if (sender is not Tower.TowerHandler || (sender as TowerHandler) != TowerHandler) return;

            BattleManager.Instance.ChangeMoney(-TowerHandler.TowerData.Cost);
            BattleManager.Instance.ChangeSalary(TowerHandler.TowerData.SalaryPerHour);
        }

        public void SellTower()
        {
            BattleManager.Instance.ChangeSalary(-TowerHandler.TowerData.SalaryPerHour);
            Destroy(TowerHandler.gameObject);
        }
    }
}
