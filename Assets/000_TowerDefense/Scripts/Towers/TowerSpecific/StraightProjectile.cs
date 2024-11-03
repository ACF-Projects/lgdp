using RushHour.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower
{
    public class StraightProjectile : Projectile
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool pierce;

        private float damage;
        private float lifeTime;

        public override void Init(EnemyHandler enemy, float speed, float damage, TowerData towerData)
        {
            this.damage = damage;
            data = towerData;
            lifeTime = (data.EffectRadius) / speed;
            rb.velocity = transform.right * speed;
        }

        private void Update()
        {
            if (!pierce) return;
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EnemyHandler>(out var enemy))
            {
                enemy.TakeDamage(damage);
                if(pierce) Destroy(gameObject);
            }
        }
    }
}
