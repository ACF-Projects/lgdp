using LGDP.TowerDefense.Lucas.POC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.POC
{
    public class TowerPOC : MonoBehaviour
    {
        [SerializeField] private TowerPlaceHandler placeHandler;
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

        private void OnEnable()
        {
            placeHandler.OnPlaced += PlaceHandler_OnPlaced;
        }

        private void OnDisable()
        {
            placeHandler.OnPlaced -= PlaceHandler_OnPlaced;
        }

        private void PlaceHandler_OnPlaced()
        {
            StartCoroutine(RefreshCollider());
        }

        private IEnumerator RefreshCollider()
        {
            var c = GetComponent<Collider2D>();
            c.enabled = false;
            yield return new WaitForFixedUpdate();
            c.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!placeHandler.IsPlaceable && collision.TryGetComponent<EnemyPOC>(out var enemy)) enemyList.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(!placeHandler.IsPlaceable && collision.TryGetComponent<EnemyPOC>(out var enemy) && enemyList.Contains(enemy)) enemyList.Remove(enemy);
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
            if(currentEnemy == null) return;
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
