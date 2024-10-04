using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower.Components
{
    public class TowerAudio : TowerComponent
    {
        public void PlayPlacementSound(bool validPlacement)
        {
            if (validPlacement)
            {
                AudioManager.Instance.PlayOneShot(TowerHandler.TowerData.PlacementSFX);
            }
            else
            {
                AudioManager.Instance.PlayOneShot(SoundEffect.InvalidPlacement);
            }
        }
    }
}
