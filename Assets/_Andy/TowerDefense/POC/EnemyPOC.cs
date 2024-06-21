using LGDP.TowerDefense.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.POC
{
    public class EnemyPOC : MonoBehaviour
    {
        public static event Action<EnemyPOC> OnEnemyHit;
        public static event Action<EnemyPOC> OnEnemyKilled;
        public static event Action<EnemyPOC> OnEndReached;

        [SerializeField] private SpriteRenderer sprite;

        private WaypointPOC waypoint;
        private EnemyData _enemyData;
        public EnemyData EnemyData => _enemyData;
        private int currentWaypoint;

        private float _currentHealth;
        public float CurrentHealth => _currentHealth;

        private bool dead;

        public void Init(WaypointPOC waypoint, EnemyData enemyData)
        {
            this.waypoint = waypoint;
            _enemyData = enemyData;
            currentWaypoint = 0;
            _currentHealth = enemyData.Health;
            dead = false;
        }

        private void Update()
        {
            CheckWaypoint();
            Rotate();
        }

        private void CheckWaypoint()
        {
            if (Vector2.Distance(transform.position, waypoint.waypoints[currentWaypoint].position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.waypoints[currentWaypoint].position, _enemyData.MoveSpeed * Time.deltaTime);
                return;
            }
            if(currentWaypoint < waypoint.waypoints.Count - 1)
            {
                currentWaypoint++;
            }
            else
            {
                OnEndReached?.Invoke(this);
                Destroy(gameObject);
            }
        }

        private void Rotate()
        {
            if (waypoint.waypoints[currentWaypoint].position.x >= transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        public void TakeDamage(float damage)
        {
            if (dead) return;
            _currentHealth -= damage;
            if(_currentHealth <= 0 && !dead)
            {
                _currentHealth = 0;
                OnEnemyKilled?.Invoke(this);
                dead = true;
                Destroy(gameObject);
            }
            else
            {
                OnEnemyHit?.Invoke(this);
            }
        }

    }
}
