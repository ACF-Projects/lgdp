using RushHour.Data;
using RushHour.POC;
using RushHour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RushHour
{
    [System.Serializable]
    public struct BurstInfo
    {
        public EnemyData EnemyData;
        public float TimeBeforeSpawn;
        public float TimeBetweenSpawns;
        public int EnemyCount;
    }

    [System.Serializable]
    public struct Wave
    {
        public List<BurstInfo> WaveBurstInfo;
    }

    public class EnemySpawner : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private WaypointPOC _waypoint;
        [Header("Wave Information")]
        public List<Wave> Waves = new();  // All wave information for this level
        public static int EnemiesRemaining  // If set to 0, invokes OnAllEnemiesDead action
        {
            get => _enemiesRemaining;
            set
            {
                _enemiesRemaining = value;
                if (_enemiesRemaining == 0)
                {
                    OnAllEnemiesDead?.Invoke();
                }
            }
        }

        [HideInInspector]
        public int CurrentWaveNumber  // Current wave number, increments every wave
        {
            get; private set;
        }

        [HideInInspector] public static Action OnAllEnemiesDead = null;  // When enemies are dead for curr wave
        [HideInInspector] public static Action OnWavesCompleted = null;  // When next wave is attempted to be spawned but all waves are finished

        private static int _enemiesRemaining = 0;

        private void Start()
        {
            SpawnNextWave();
            OnWavesCompleted += () =>  // TODO: This is for testing purposes; can remove
            {
                Debug.Log("All waves are done!");
                TransitionManager.Instance.GoToScene("TD_TITLE");
            };
        }

        private void OnEnable()
        {
            OnAllEnemiesDead += SpawnNextWave;
            EnemyHandler.OnEndReached += DecrementEnemiesRemaining;
            EnemyHandler.OnEnemyKilled += DecrementEnemiesRemaining;
        }

        private void OnDisable()
        {
            OnAllEnemiesDead -= SpawnNextWave;
            EnemyHandler.OnEndReached -= DecrementEnemiesRemaining;
            EnemyHandler.OnEnemyKilled -= DecrementEnemiesRemaining;
        }

        private void DecrementEnemiesRemaining(EnemyHandler enemy) => EnemiesRemaining--;

        /// <summary>
        /// Increments the current wave number and begins spawning the
        /// next wave of enemies.
        /// 
        /// Invokes `OnWavesCompleted` if no more waves (index out of bounds).
        /// </summary>
        private void SpawnNextWave()
        {
            CurrentWaveNumber++;
            if (CurrentWaveNumber - 1 >= Waves.Count)
            {
                OnWavesCompleted?.Invoke();
                return;
            }
            Wave currWave = Waves[CurrentWaveNumber - 1];
            foreach (BurstInfo burst in currWave.WaveBurstInfo)
            {
                StartCoroutine(SpawnBursts(burst));
            }
        }

        /// <summary>
        /// Given data for a burst of enemies, spawns the entire burst
        /// of enemies over a duration.
        /// </summary>
        private IEnumerator SpawnBursts(BurstInfo burst)
        {
            yield return new WaitForSeconds(burst.TimeBeforeSpawn);
            for (int i = 0; i < burst.EnemyCount; i++)
            {
                var enemyObj = Instantiate(burst.EnemyData.enemyPrefab, _waypoint.waypoints[0].position, Quaternion.identity).GetComponent<EnemyHandler>();
                enemyObj.Init(_waypoint, burst.EnemyData);
                _enemiesRemaining++;
                yield return new WaitForSeconds(burst.TimeBetweenSpawns);
            }
        }

    }
}
