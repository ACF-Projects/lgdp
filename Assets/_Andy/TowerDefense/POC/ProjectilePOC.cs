using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.POC
{
    public class ProjectilePOC : MonoBehaviour
    {
        private EnemyPOC enemy;
        private float speed;
        private float damage;
        public void Init(EnemyPOC enemy, float speed, float damage)
        {
            this.enemy = enemy;
            this.speed = speed;
            this.damage = damage;
        }

        private void Update()
        {
            if(enemy == null)
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
            if(Vector2.Distance(transform.position, enemy.transform.position) <= 0.1f)
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
