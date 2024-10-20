using NaughtyAttributes;
using RushHour.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour.UserInterface
{
    [Serializable]
    public class StatSlot
    {
        [ReadOnly] public StatType type;
        public TextMeshProUGUI nameField;
        public TextMeshProUGUI valueField;
    }

    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField] private StatSlot[] statSlots;

        public void SetStats(TowerStats stats)
        {
            SetStat(StatType.MainValue, TowerStat<float>.ConvertToString(stats.mainValue));
            SetStat(StatType.Range, TowerStat<float>.ConvertToString(stats.range));
            SetStat(StatType.Cost, TowerStat<float>.ConvertToString(stats.cost));
            SetStat(StatType.HitSpeed, TowerStat<float>.ConvertToString(stats.hitSpeed));
            SetStat(StatType.TargetType, stats.targetType);
            SetStat(StatType.StrongAgainst, TowerStat<EnemyType>.ConvertToString(stats.strongAgainst));
            SetStat(StatType.WeakAgainst, TowerStat<EnemyType>.ConvertToString(stats.weakAgainst));
        }

        private void SetStat(StatType type, TowerStat<string> towerStat)
        {
            var slot = Array.Find(statSlots, x => x.type == type);
            if(slot.nameField != null)
            {
                slot.nameField.text = towerStat.name;
            }
            if(slot.valueField != null)
            {
                slot.valueField.text = towerStat.value;
            }
        }

        public void ClearStats()
        {
            foreach (var slot in statSlots)
            {
                if(slot.nameField != null)
                {
                    slot.nameField.text = "";
                }
                if(slot.valueField != null)
                {
                    slot.valueField.text = "";
                }
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            int numSlots = Enum.GetNames(typeof(StatType)).Length;
            if (statSlots == null || statSlots.Length != numSlots){
                statSlots = new StatSlot[numSlots];
                for (int i = 0; i < numSlots; ++i)
                {
                    statSlots[i] = new()
                    {
                        type = (StatType)i
                    };
                }
            }

            
        }
#endif
    }
}
