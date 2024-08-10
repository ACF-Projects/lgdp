using LGDP.TowerDefense.POC;
using LGDP.TowerDefense;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class TowerHandler : MonoBehaviour
    {

        [SerializeField] private Transform visual;
        [SerializeField] private float cooldown;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;

        [SerializeField] private ProjectilePOC projectile;

        private List<EnemyPOC> enemyList;

        private EnemyPOC currentEnemy;

        private float currentTimer;

        private void Awake()
        {
            enemyList = new();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyPOC>(out var enemy)) enemyList.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyPOC>(out var enemy) && enemyList.Contains(enemy)) enemyList.Remove(enemy);
        }

        private void Update()
        {
            GetCurrentEnemy();
            FaceEnemy();
            Attack();
        }

        private void GetCurrentEnemy()
        {
            if (enemyList.Count <= 0)
            {
                currentEnemy = null;
                return;
            }

            currentEnemy = enemyList[0];
        }

        private void FaceEnemy()
        {
            if (currentEnemy == null) return;
            Vector3 targetPos = currentEnemy.transform.position - transform.position;
            float angle = Vector3.SignedAngle(visual.up, targetPos, visual.forward);

            visual.Rotate(0, 0, angle);
        }

        private void Attack()
        {
            if (currentEnemy == null) return;
            if (currentTimer > 0f) currentTimer -= Time.deltaTime;
            else
            {
                var proj = Instantiate(projectile, transform.position, Quaternion.identity);
                proj.Init(currentEnemy, projectileSpeed, damage);
                currentTimer = cooldown;
            }
        }
    }
}
