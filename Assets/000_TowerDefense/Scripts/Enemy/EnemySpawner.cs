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
    public struct Wave
    {
        public EnemyData EnemyData;
        public float TimeBeforeSpawn;
        public float TimeBetweenSpawns;
        public int EnemyCount;
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

        public static bool CanSpawnEnemies = true;

        [HideInInspector] public static Action OnAllEnemiesDead = null;  // When enemies are dead for curr wave

        private static int _enemiesRemaining = 0;

        private void Start()
        {
            StartWaves();  // Start waves of enemies
        }

        private void OnEnable()
        {
            EnemyHandler.OnEndReached += DecrementEnemiesRemaining;
            EnemyHandler.OnEnemyKilled += DecrementEnemiesRemaining;
        }

        private void OnDisable()
        {
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
        private void StartWaves()
        {
            for (int i = 0; i < Waves.Count; i++)
            {
                StartCoroutine(SpawnWave(Waves[i]));
            }
        }

        /// <summary>
        /// Given data for a burst of enemies, spawns the entire burst
        /// of enemies over a duration.
        /// </summary>
        private IEnumerator SpawnWave(Wave wave)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => TimerTicker.CanTick);  // Wait until timers are activated
            yield return new WaitUntil(() => CanSpawnEnemies);  // If can't spawn enemies, wait
            yield return new WaitForSeconds(wave.TimeBeforeSpawn);
            for (int i = 0; i < wave.EnemyCount; i++)
            {
                yield return new WaitUntil(() => CanSpawnEnemies);  // If can't spawn enemies, wait
                var enemyObj = Instantiate(wave.EnemyData.enemyPrefab, _waypoint.waypoints[0].position, Quaternion.identity).GetComponent<EnemyHandler>();
                enemyObj.Init(_waypoint, wave.EnemyData);
                _enemiesRemaining++;
                yield return new WaitForSeconds(wave.TimeBetweenSpawns);
            }
        }

    }
}
