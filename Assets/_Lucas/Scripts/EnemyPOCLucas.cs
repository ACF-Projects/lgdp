using LGDP.TowerDefense.Data;
using LGDP.TowerDefense.POC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.Lucas.POC
{
    public class EnemyPOCLucas : MonoBehaviour
    {
        public static event Action<EnemyPOCLucas> OnEnemyHit;
        public static event Action<EnemyPOCLucas> OnEnemyKilled;
        public static event Action<EnemyPOCLucas> OnEndReached;

        [SerializeField] private SpriteRenderer sprite;

        private WaypointPOC waypoint;
        private EnemyData _enemyData;
        public EnemyData EnemyData => _enemyData;
        private int currentWaypoint;

        private float _currentHealth;
        public float CurrentHealth => _currentHealth;

        public bool IsDead { get; private set; }

        public void Init(WaypointPOC waypoint, EnemyData enemyData)
        {
            this.waypoint = waypoint;
            _enemyData = enemyData;
            currentWaypoint = 0;
            _currentHealth = enemyData.Health;
            IsDead = false;
        }

        private void Update()
        {
            if (!IsDead)
            {
                CheckWaypoint();
                Rotate();
            }
        }

        private void CheckWaypoint()
        {
            if (Vector2.Distance(transform.position, waypoint.waypoints[currentWaypoint].position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.waypoints[currentWaypoint].position, _enemyData.MoveSpeed * Time.deltaTime);
                return;
            }
            if (currentWaypoint < waypoint.waypoints.Count - 1)
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
            if (IsDead) return;
            _currentHealth -= damage;
            if (_currentHealth <= 0 && !IsDead)
            {
                _currentHealth = 0;
                OnEnemyKilled?.Invoke(this);
                IsDead = true;
                // If this enemy should go to the store, make them do that
                if (TryGetComponent(out EnemyGoToStore enemyGoToStore))
                {
                    enemyGoToStore.TrackStore();
                    // Then, disable this sprite (no longer trackable by towers)
                    enabled = false;
                }
            }
            else
            {
                OnEnemyHit?.Invoke(this);
            }
        }

    }
}
