using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower.Components
{
    public class TowerActivator : TowerComponent
    {
        public override void Init(TowerHandler handler)
        {
            base.Init(handler);

            TowerMove.OnTowerGrabbed += TowerMove_OnTowerGrabbed;
            TowerMove.OnTowerDropped += TowerMove_OnTowerDropped;
        }

        private void OnDestroy()
        {
            TowerMove.OnTowerGrabbed -= TowerMove_OnTowerGrabbed;
            TowerMove.OnTowerDropped -= TowerMove_OnTowerDropped;
        }

        private void TowerMove_OnTowerDropped(object sender, bool e)
        {
            if (sender is not Tower.TowerHandler || (sender as TowerHandler) != TowerHandler) return;

            TowerHandler.IsActivated = true;
        }

        private void TowerMove_OnTowerGrabbed(object sender, System.EventArgs e)
        {
            if (sender is not Tower.TowerHandler || (sender as TowerHandler) != TowerHandler) return;

            TowerHandler.IsActivated = false;
        }
    }
}
