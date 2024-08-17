using LGDP.TowerDefense.POC;
using LGDP.TowerDefense;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGDP.TowerDefense.Data;

namespace RushHour
{
    public class TowerHandler : MonoBehaviour
    {

        [SerializeField] private Transform visual;
        [SerializeField] private Transform effectPreview;
        [SerializeField] private CircleCollider2D effectTrigger;
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

        /// <summary>
        /// Initializes this tower's data.
        /// </summary>
        public void Init(TowerData towerData)
        {
            visual.localScale = towerData.SpriteScale;
            effectPreview.GetComponent<SpriteRenderer>().sprite = towerData.EffectPreviewSprite;
            float spriteDiameter = effectPreview.GetComponent<SpriteRenderer>().bounds.size.x; // Assuming the sprite is a circle
            float scaleFactor = towerData.EffectRadius * 2f / spriteDiameter;
            effectPreview.localScale = new Vector3(scaleFactor, scaleFactor);
            effectTrigger.radius = towerData.EffectRadius;
        }

        private void Update()
        {
            TargetEnemy();
            FaceEnemy();
            TryAttack();
        }

        /// <summary>
        /// Searches through cached enemy list, sets the `currentEnemy`
        /// variable to the next enemy this tower should target.
        /// </summary>
        private void TargetEnemy()
        {
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
            float angle = Vector3.SignedAngle(visual.up, targetPos, visual.forward);

            visual.Rotate(0, 0, angle);
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
            }
        }
    }
}
