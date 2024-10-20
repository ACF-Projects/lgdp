using RushHour.Data;
using UnityEngine;

namespace RushHour.Tower
{
    public class Projectile : MonoBehaviour
    {
        private EnemyHandler enemy;
        private TowerData data;
        private float speed;
        private float damage;
        public void Init(EnemyHandler enemy, float speed, float damage, TowerData towerData)
        {
            this.enemy = enemy;
            this.speed = speed;
            this.damage = damage;
            this.data = towerData;
        }

        private void Update()
        {
            if (enemy == null)
            {
                Destroy(gameObject);
                return;
            }
            Move();
            Rotate();
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemy.transform.position) <= 0.1f)
            {
                enemy.TakeDamage(damage, data.StrongAgainst, data.WeakAgainst);
                Destroy(gameObject);
            }
        }

        private void Rotate()
        {
            if (this.gameObject == null) return;
            Vector3 targetPos = enemy.transform.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
            transform.Rotate(0, 0, angle);
        }
    }
}
