using System.Collections.Generic;
using UnityEngine;
using LGDP.TowerDefense.Data;
using DG.Tweening;
using System.Linq;

namespace LGDP.TowerDefense.Lucas.POC
{
    public class TowerHandler : MonoBehaviour
    {

        [SerializeField] private Transform visual;
        [SerializeField] private Transform effectPreview;
        [SerializeField] private CircleCollider2D effectTrigger;
        [SerializeField] private float cooldown;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;

        [SerializeField] private ProjectilePOCLucas projectile;

        private List<EnemyPOCLucas> enemyList;

        private EnemyPOCLucas currentEnemy;

        private float currentTimer;

        private const float EFFECT_ALPHA_ON_HOVER = 0.3f;  // Opacity of effect circle when shown

        private void Awake()
        {
            enemyList = new();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyPOCLucas>(out var enemy)) enemyList.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<EnemyPOCLucas>(out var enemy) && enemyList.Contains(enemy)) enemyList.Remove(enemy);
        }

        /// <summary>
        /// Initializes this tower's data.
        /// 
        /// Also logs this unit's salary into Globals.
        /// </summary>
        public void Init(TowerData towerData)
        {
            visual.localScale = towerData.SpriteScale;
            effectPreview.GetComponent<SpriteRenderer>().sprite = towerData.EffectPreviewSprite;
            float spriteDiameter = effectPreview.GetComponent<SpriteRenderer>().bounds.size.x; // Assuming the sprite is a circle
            float scaleFactor = towerData.EffectRadius * 2f / spriteDiameter;
            effectPreview.localScale = new Vector3(scaleFactor, scaleFactor);
            effectTrigger.radius = towerData.EffectRadius;

            // Log salary per hour
            Globals.SalaryPerHour += towerData.SalaryPerHour;
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

        public void OnMouseEnter()
        {
            ShowEffectRadius();  // TODO: This is for dev purposes. Not necessarily what we want to do
        }

        public void OnMouseExit()
        {
            HideEffectRadius();  // TODO: This is for dev purposes. Not necessarily what we want to do
        }

        public void ShowEffectRadius()
        {
            SpriteRenderer previewRenderer = effectPreview.GetComponent<SpriteRenderer>();
            DOTween.To(() => previewRenderer.color.a, 
                x => previewRenderer.color = new Color(previewRenderer.color.r, previewRenderer.color.g, previewRenderer.color.b, x), 
                EFFECT_ALPHA_ON_HOVER, 0.2f).SetEase(Ease.OutSine);
        }

        public void HideEffectRadius()
        {
            SpriteRenderer previewRenderer = effectPreview.GetComponent<SpriteRenderer>();
            DOTween.To(() => previewRenderer.color.a,
                x => previewRenderer.color = new Color(previewRenderer.color.r, previewRenderer.color.g, previewRenderer.color.b, x),
                0, 0.2f).SetEase(Ease.OutSine);
        }

    }
}
