using LGDP.TowerDefense.Lucas.POC;
using UnityEngine;

namespace LGDP.TowerDefense.Lucas.POC
{
    public class ProjectilePOCLucas : MonoBehaviour
    {
        private EnemyPOCLucas enemy;
        private float speed;
        private float damage;
        public void Init(EnemyPOCLucas enemy, float speed, float damage)
        {
            this.enemy = enemy;
            this.speed = speed;
            this.damage = damage;
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
                enemy.TakeDamage(damage);
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
