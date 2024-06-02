using LGDP.TowerDefense.Data;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class TowerStore : MonoBehaviour
    {

        [field: SerializeField] public List<TowerData> AllTowerData = new();

#if UNITY_EDITOR
        [Button]
        public void RefreshTowerData()
        {
            AllTowerData = new List<TowerData>();
            string[] guids = AssetDatabase.FindAssets("t:TowerData");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TowerData obj = AssetDatabase.LoadAssetAtPath<TowerData>(path);
                if (obj != null)
                {
                    AllTowerData.Add(obj);
                }
            }
        }
#endif

    }
}
