using RushHour.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower
{
    public abstract class Projectile : MonoBehaviour
    {
        protected TowerData data;
        public abstract void Init(EnemyHandler enemy, float speed, float damage, TowerData towerData);
    }
}
