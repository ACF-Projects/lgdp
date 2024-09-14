using System.Collections.Generic;
using UnityEngine;
using RushHour.Data;
using DG.Tweening;
using NaughtyAttributes;

namespace RushHour.Tower
{
    public abstract class TowerHandler : MonoBehaviour
    {
        // List of Tower Components
        [SerializeField, ReorderableList, ReadOnly] protected TowerComponent[] _towerComponents;

        protected readonly Dictionary<string, TowerComponent> towerComponents = new();

        [Header("References")]
        [SerializeField] protected Transform visual;

        [HideInInspector] public bool IsActivated = false;  // No tower logic renders if `false`

        private TowerData _towerData;
        public TowerData TowerData => _towerData;

        /// <summary>
        /// Initializes this tower's data.
        /// 
        /// Also logs this unit's salary into Globals.
        /// </summary>
        public void Init(TowerData towerData)
        {
            _towerData = towerData;

            foreach (var component in _towerComponents)
            {
                string key = component.GetType().Name;

                if (!towerComponents.ContainsKey(key))
                {
                    towerComponents.Add(key, component);
                }

                component.Init(this);
            }

            visual.GetComponent<SpriteRenderer>().sprite = TowerData.TowerSprite;
            visual.localScale = TowerData.SpriteScale;
        }

        public T GetTowerComponent<T>() where T : TowerComponent
        {
            towerComponents.TryGetValue(typeof(T).Name, out var component);
            return component as T;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            _towerComponents = GetComponentsInChildren<TowerComponent>();
            foreach (var component in _towerComponents)
            {
                component.TowerHandler = this;
            }
        }
#endif

    }
}