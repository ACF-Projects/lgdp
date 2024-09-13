using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower
{
    public abstract class TowerComponent : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] public TowerHandler TowerHandler { get; set; }

        public virtual void Init(TowerHandler handler)
        {
            TowerHandler = handler;
        }
    }
}
