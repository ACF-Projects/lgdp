using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.Data
{
    public abstract class EnemyData : ScriptableObject
    {
        [field: SerializeField] public float Health { get; protected set; }
        [field: SerializeField] public float Damage { get; protected set; }
        [field: SerializeField] public float MoveSpeed { get; protected set; }
        public GameObject enemyPrefab;
    }
}
