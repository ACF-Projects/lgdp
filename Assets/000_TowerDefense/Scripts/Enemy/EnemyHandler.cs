using RushHour.Data;
using RushHour.Global;
using RushHour.POC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class EnemyHandler : MonoBehaviour
    {
        public static event Action<EnemyHandler> OnEnemyHit;
        public static event Action<EnemyHandler> OnEnemyKilled;
        public static event Action<EnemyHandler> OnEndReached;
        public static event Action<EnemyHandler> OnStoreReached;

        [SerializeField] private SpriteRenderer sprite;

        private WaypointPOC waypoint;
        private EnemyData _enemyData;
        public EnemyData EnemyData => _enemyData;
        private int currentWaypoint;

        private float _currentHealth;
        public float CurrentHealth => _currentHealth;

        public bool IsDead { get; private set; }

        private float speedMultiplier;

        private List<SpeedModifier> speedModifiers = new();

        private class SpeedModifier
        {
            public float multiplier;
            public float duration;
        }

        private class SpeedModifierCompare : IComparer<SpeedModifier>
        {
            public int Compare(SpeedModifier x, SpeedModifier y)
            {
                if(x.multiplier > y.multiplier)
                {
                    return -1;
                }
                else if(x.multiplier < y.multiplier)
                {
                    return 1;
                }
                else
                {
                    if(x.duration < y.duration)
                    {
                        return -1;
                    }
                    else if(x.duration > y.duration)
                    {
                        return 1;
                    }

                    return 0;
                }
            }
        }

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
                UpdateSlows();
            }
        }

        private void UpdateSlows()
        {
            speedMultiplier = 0;
            float dt = Time.deltaTime;
            for (int i = speedModifiers.Count - 1; i >= 0; --i)
            {
                speedModifiers[i].duration -= dt;
                if (speedModifiers[i].duration < 0) speedModifiers.RemoveAt(i);
            }
            if (speedModifiers.Count == 0) return;
            speedMultiplier = speedModifiers[0].multiplier;
        }

        private void CheckWaypoint()
        {
            if (Vector2.Distance(transform.position, waypoint.waypoints[currentWaypoint].position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.waypoints[currentWaypoint].position, _enemyData.MoveSpeed * (1f - speedMultiplier) * Time.deltaTime);
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

        public void TakeDamage(float damage, EnemyType weaknesses = 0, EnemyType resistances = 0)
        {
            if (IsDead) return;
            
            // Only compare flags if it's not None
            if (EnemyData.Type != EnemyType.None)
            {
                if (weaknesses.HasFlag(EnemyData.Type))
                {
                    damage *= Constants.WEAKNESS_DAMAGE_MULTIPLIER;
                }
                else if (resistances.HasFlag(EnemyData.Type))
                {
                    damage *= Constants.RESISTANCE_DAMAGE_MULTIPLIER;
                }
            }

            _currentHealth -= damage;
            if (_currentHealth <= 0 && !IsDead)
            {
                _currentHealth = 0;
                OnEnemyHit?.Invoke(this);
                OnEnemyKilled?.Invoke(this);
                IsDead = true;
                // If this enemy should go to the store, make them do that
                if (TryGetComponent(out EnemyGoToStore enemyGoToStore))
                {
                    transform.rotation = Quaternion.Euler(0, 0, -sprite.transform.rotation.eulerAngles.z);
                    sprite.transform.localRotation = Quaternion.Euler(0, 0, 180);  // Rotate so correctly tracks store
                    enemyGoToStore.TrackStore(this);
                    // Then, disable this sprite (no longer trackable by towers)
                    enabled = false;
                }
            }
            else
            {
                OnEnemyHit?.Invoke(this);
            }
        }

        public void ReachedStore()
        {
            OnStoreReached?.Invoke(this);
        }

        public void AddSlow((float, float) slowEffect)
        {
            speedModifiers.Add(new SpeedModifier() { multiplier = slowEffect.Item1, duration = slowEffect.Item2});
            speedModifiers.Sort(new SpeedModifierCompare());
        }

    }
}
