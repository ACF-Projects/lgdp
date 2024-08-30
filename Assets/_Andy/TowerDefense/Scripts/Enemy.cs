using RushHour.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class Enemy : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemyHit;
        public static event Action<Enemy> OnEnemyKilled;
        public static event Action<Enemy> OnEndReached;

        private Waypoint waypoint;
        private EnemyData _enemyData;

        public void Init(Waypoint waypoint, EnemyData enemyData)
        {
            this.waypoint = waypoint;
            _enemyData = enemyData;
        }


    }
}
