using LGDP.TowerDefense.Data;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.POC
{
    [System.Serializable]
    public class EnemyBurst
    {
        public int enemyCount;
        public float timeBetweenEnemy;
        public float interval;
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyBurst[] bursts;
        public float interval;
    }

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyData enemy;
        [SerializeField] private WaypointPOC waypoint;
        [SerializeField, ReorderableList] private Wave[] waves;

        private int currentWave;

        private void Awake()
        {
            currentWave = 0;
            Invoke(nameof(SpawnWave), 5);
        }

        private void SpawnWave()
        {
            StartCoroutine(SpawnBursts(currentWave));

            currentWave++;
            if (currentWave >= waves.Length)
            {
                return;
            }
            Invoke(nameof(SpawnWave), waves[currentWave].interval);
        }

        private IEnumerator SpawnBursts(int waveNumber)
        {
            foreach (var burst in waves[waveNumber].bursts)
            {
                for (int i = 0; i < burst.enemyCount; i++)
                {
                    var enemyObj = Instantiate(enemy.enemyPrefab, waypoint.waypoints[0].position, Quaternion.identity).GetComponent<EnemyPOC>();
                    enemyObj.Init(waypoint, enemy);
                    ManagerPOC.instance.enemyCount++;
                    yield return new WaitForSeconds(burst.timeBetweenEnemy);
                }
                yield return new WaitForSeconds(burst.interval);
            }
            if(waveNumber == waves.Length - 1)
            {
                ManagerPOC.instance.FinishedWaves();
            }
        }
    }
}
