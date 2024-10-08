using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower.Components
{
    public class TowerAudio : TowerComponent
    {
        public void PlayPlacementSound(bool validPlacement)
        {
            // If there is no sound assigned, don't play any sound
            if (TowerHandler.TowerData.PlacementSFX == null)
            {
                return;
            }
            // Or else, play corresponding sound depending on if it was a valid placement
            if (validPlacement)
            {
                AudioManager.Instance.PlayOneShot(TowerHandler.TowerData.PlacementSFX, TowerHandler.TowerData.PlacementSFXVolume);
            }
            else
            {
                AudioManager.Instance.PlayOneShot(SoundEffect.InvalidPlacement);
            }
        }
    }
}
