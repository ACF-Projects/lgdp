using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LGDP.TowerDefense.POC
{
    public class ManagerPOC : MonoBehaviour
    {
        public static ManagerPOC instance;

        [SerializeField] private int lives;
        [SerializeField] private TextMeshProUGUI lifeCount;

        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        [ReadOnly] public int enemyCount;

        private bool finishedWaves;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            finishedWaves = false;
            
            Globals.Money = 120;
            lifeCount.text = $"Lives: {lives}";
        }

        private void OnEnable()
        {
            EnemyPOC.OnEnemyKilled += EnemyPOC_OnEnemyKilled;
            EnemyPOC.OnEndReached += EnemyPOC_OnEndReached;
        }

        private void OnDisable()
        {
            EnemyPOC.OnEnemyKilled -= EnemyPOC_OnEnemyKilled;
            EnemyPOC.OnEndReached -= EnemyPOC_OnEndReached;
        }

        private void EnemyPOC_OnEndReached(EnemyPOC obj = null)
        {
            lives--;
            enemyCount--;
            lifeCount.text = $"Lives: {lives}";
            if(lives <= 0)
            {
                Time.timeScale = 0f;
                loseScreen.SetActive(true);
                return;
            }
            if (finishedWaves && enemyCount <= 0)
            {
                Time.timeScale = 0f;
                winScreen.SetActive(true);
            }
        }

        private void EnemyPOC_OnEnemyKilled(EnemyPOC obj)
        {
            Globals.Money += 15;
            enemyCount--;
            if (finishedWaves && enemyCount <= 0)
            {
                Time.timeScale = 0f;
                winScreen.SetActive(true);
            }
        }

        private void Update()
        {
            if (finishedWaves && enemyCount <= 0)
            {
                Time.timeScale = 0f;
                winScreen.SetActive(true);
            }
        }

        public void FinishedWaves()
        {
            finishedWaves = true;
            print("done");
        }
    }
}
