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

        public static TowerStore Instance;

        public List<TowerData> AllTowerData;

        #region Singleton Logic
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            Instance = this;
        }
        #endregion

#if UNITY_EDITOR
        /// <summary>
        /// Searches through the entire project for files of type "TowerData".
        /// Then, clears and adds all these to the `AllTowerData` list.
        /// </summary>
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
