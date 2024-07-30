using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP
{
    /// <summary>
    /// Given two endpoints, spawns people prefabs that move from
    /// a randomly chosen endpoint to the other.
    /// </summary>
    public class TitlePeople : MonoBehaviour
    {

        [Header("Endpoint Assignments")]
        [SerializeField] private Transform _endpoint1;
        [SerializeField] private Transform _endpoint2;
        [Header("People Properties")]
        [SerializeField] private float _spawnPosYVariation;  // Variation in people spawn positions
        [Header("Object Assignments")]
        [SerializeField] private Transform _personContainer;
        [Header("Prefab Assignments")]
        [SerializeField] private GameObject _personPrefab;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnPerson), 0, 1);
        }

        /// <summary>
        /// Instantiates a person and moves them from a
        /// random endpoint to the other.
        /// </summary>
        public void SpawnPerson()
        {
            StartCoroutine(SpawnAndMovePersonCoroutine());
        }

        private IEnumerator SpawnAndMovePersonCoroutine()
        {
            GameObject personObj = Instantiate(_personPrefab, _personContainer);

            float rand = Random.Range(0f, 1f);
            Vector3 startPos = rand < 0.5f ? _endpoint1.position : _endpoint2.position;
            Vector3 endPos = rand < 0.5f ? _endpoint2.position : _endpoint1.position;
            float yVariation = Random.Range(-_spawnPosYVariation, _spawnPosYVariation);
            startPos += new Vector3(0f, yVariation);
            endPos += new Vector3(0f, yVariation); 
            float currTime = 0f;
            float timeToWait = Random.Range(5f, 8f);  // Time for this person to reach the end
            while (currTime < timeToWait)
            {
                currTime += Time.deltaTime;
                personObj.transform.position = Vector3.Lerp(startPos, endPos, currTime / timeToWait);
                yield return null;
            }

            Destroy(personObj);  // Not caring about performance of this feature
        }

    }
}
