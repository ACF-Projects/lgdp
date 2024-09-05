using RushHour.Data;
using RushHour.POC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace RushHour
{
    public class EnemyHandler : MonoBehaviour
    {
        public static event Action<EnemyHandler> OnEnemyHit;
        public static event Action<EnemyHandler> OnEnemyKilled;
        public static event Action<EnemyHandler> OnEndReached;

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
            Vector3 relativeTarget = (waypoint.waypoints[currentWaypoint].position - transform.position).normalized;
            Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.down, relativeTarget);
            sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, toQuaternion, 3 * Time.deltaTime);
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
                    sprite.flipX = true;  // Flip so sprite correctly tracks store
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
