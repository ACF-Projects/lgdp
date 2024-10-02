using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Data
{
    [CreateAssetMenu(menuName = "TowerDefense/EnemyData", fileName = "New EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public float Health { get; protected set; }
        [field: SerializeField] public float Damage { get; protected set; }
        [field: SerializeField] public float MoveSpeed { get; protected set; }

        [field: SerializeField] public float Reward { get; protected set; }

        public GameObject enemyPrefab;
    }
}
