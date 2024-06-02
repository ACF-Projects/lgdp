using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense.Data
{
    public abstract class TowerData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField, ResizableTextArea] public string Description { get; protected set; }
        [field: SerializeField] public float Cost { get; protected set; }
        [field: SerializeField] public float PlacementRadius { get; protected set; }

        public GameObject towerPrefab;
    }
}
