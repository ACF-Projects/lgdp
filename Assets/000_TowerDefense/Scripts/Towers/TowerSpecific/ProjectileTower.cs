using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class ProjectileTower : TowerHandler
    {

        [Header("Projectile Properties")]
        [SerializeField] private float cooldown;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private Projectile projectile;
        [SerializeField] private AudioClip projectileShootSFX;
        [SerializeField] private float projectileSFXVolume;

        private List<EnemyHandler> enemyList = new();
        private EnemyHandler currentEnemy;
        private float currentTimer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyHandler>(out var enemy)) enemyList.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyHandler>(out var enemy) && enemyList.Contains(enemy)) enemyList.Remove(enemy);
        }

        private void Update()
        {
            if (IsActivated)
            {
                TargetEnemy();
                FaceEnemy();
                TryAttack();
            }
        }

        /// <summary>
        /// Searches through cached enemy list, sets the `currentEnemy`
        /// variable to the next enemy this tower should target.
        /// </summary>
        private void TargetEnemy()
        {
            // If enemies are dead in the trigger list, delete them
            enemyList = enemyList.FindAll((e) => !e.IsDead);
            if (enemyList.Count <= 0)
            {
                currentEnemy = null;
                return;
            }
            currentEnemy = enemyList[0];
        }

        /// <summary>
        /// Makes this tower point towards the `currentEnemy`.
        /// </summary>
        private void FaceEnemy()
        {
            if (currentEnemy == null) return;
            Vector3 targetPos = currentEnemy.transform.position - transform.position;

            // Add 180 to the angle to account for offset (unit will be facing opposite way otherwise)
            float angle = Vector3.SignedAngle(Visual.up, targetPos, Visual.forward) + 180f;

            Visual.Rotate(0, 0, angle);
        }

        /// <summary>
        /// Makes this tower attack the `currentEnemy` if its attack
        /// cooldown has refreshed. Or else, does nothing.
        /// </summary>
        private void TryAttack()
        {
            if (currentEnemy == null) return;
            if (currentTimer > 0f) currentTimer -= Time.deltaTime;
            else
            {
                var proj = Instantiate(projectile, transform.position, Quaternion.identity);
                proj.Init(currentEnemy, projectileSpeed, damage);
                currentTimer = cooldown;
                // Play sound effect
                if (projectileShootSFX != null)
                {
                    AudioManager.Instance.PlayOneShot(projectileShootSFX, projectileSFXVolume);
                }
            }
        }

    }
}
