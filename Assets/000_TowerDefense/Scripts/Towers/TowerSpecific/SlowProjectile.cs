using System;
using System.Collections;
using System.Collections.Generic;
using RushHour.Data;
using UnityEngine;

namespace RushHour.Tower
{
    public class SlowProjectile : Projectile
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField, Tooltip("Percentage Slow (e.g. 0.2 = 20% slow)")] private float slowMultiplier;
        [SerializeField] private float slowDuration;
        [SerializeField] private float additionalRange;

        private float damage;
        private float lifeTime;

        public override void Init(EnemyHandler enemy, float speed, float damage, TowerData towerData)
        {
            this.damage = damage;
            this.data = towerData;
            lifeTime = (data.EffectRadius + additionalRange) / speed;
            if(enemy == null)
            {
                Destroy(gameObject);
                return;
            }
            rb.velocity = (enemy.transform.position - transform.position).normalized * speed;
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EnemyHandler>(out var enemy))
            {
                enemy.AddSlow((slowMultiplier, slowDuration));
                enemy.TakeDamage(damage);
            }
        }
    }
}
