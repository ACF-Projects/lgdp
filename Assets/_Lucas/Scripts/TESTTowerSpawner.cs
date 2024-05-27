using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.TowerDefense
{
    public class TESTTowerSpawner : MonoBehaviour
    {

        [Header("Prefab Assignments")]
        [SerializeField] private GameObject _towerPrefab;

        private void Update()
        {
            if (!TowerPlaceHandler.IsPlacingTower && Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_towerPrefab);
            }
        }

    }
}
