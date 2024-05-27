using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class TowerPlaceHandler : MonoBehaviour
    {

        public static bool IsPlacingTower = false;

        [Header("Tower Properties")]
        public float TowerPlaceRadius = 0.6f;  // Radius to check for collisions to allow placement
        public bool IsPlaceable = true;

        private void Start()
        {
            IsPlacingTower = true;
        }

        private void Update()
        {
            if (IsPlaceable)
            {
                RenderTowerMove();
            }
            if (IsPlaceable && Input.GetMouseButtonDown(0))
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
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, TowerPlaceRadius, LayerMask.GetMask("Blocked"));
            if (col.Length != 0) return;  // If colliding with blockable, do not place
            // Or else, place this tower down here
            IsPlaceable = false;
            IsPlacingTower = false;
            Globals.Money -= 100;  // TODO: This is for testing!
        }

        /// <summary>
        /// Draws sphere around the tower to show the radius in which
        /// collisions are checked.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (IsPlaceable)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, TowerPlaceRadius);
            }
        }

    }
}
