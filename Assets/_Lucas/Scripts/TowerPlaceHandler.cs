using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP
{
    public class TowerPlaceHandler : MonoBehaviour
    {

        public bool IsPlaceable = true;

        private void Update()
        {
            if (IsPlaceable)
            {
                RenderTowerMove();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryPlaceTower();
            }
        }

        /// <summary>
        /// Move this object to the mouse position. 
        /// Should be called every frame.
        /// </summary>
        private void RenderTowerMove()
        {
            Vector3 posToMoveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posToMoveTo.z = 0;
            transform.position = posToMoveTo;
        }

        /// <summary>
        /// Place down this tower at the mouse's position.
        /// Should perform an intermediate check to see if it's placeable.
        /// </summary>
        private void TryPlaceTower()
        {
            IsPlaceable = false;
        }

    }
}
