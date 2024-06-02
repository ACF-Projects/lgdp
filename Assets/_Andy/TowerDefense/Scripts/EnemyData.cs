using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.Data
{
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public float Health;
        [field: SerializeField] public float Damage;
        [field: SerializeField] public float MoveSpeed;
    }
}
