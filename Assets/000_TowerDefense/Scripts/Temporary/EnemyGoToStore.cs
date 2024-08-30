using RushHour.POC;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    /// <summary>
    /// Makes this sprite navigate towards an object in the scene that
    /// has the tag of `Store`. Deletes the sprite after it has gotten
    /// close enough to its destination.
    /// 
    /// Starts initially disabled, but can be enabled at any time by a script. 
    /// </summary>
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(AIDestinationSetter))]
    public class EnemyGoToStore : MonoBehaviour
    {

        private GameObject _storeObject;  // Locates at the beginning of the game
        private AIPath _path;
        private AIDestinationSetter _destinationSetter;

        private const float DIST_FROM_STORE_UNTIL_DISAPPEAR = 1;  // Distance away from store this needs to get to before disappearing

        /// <summary>
        /// Disables pathfinding initially (waits until TrackStore() is called).
        /// Also locates the store object. Will log an error if conditions are not right.
        /// </summary>
        private void Awake()
        {
            _path = GetComponent<AIPath>();
            _destinationSetter = GetComponent<AIDestinationSetter>();
            _path.enabled = false;  // Initially disable pathfinding

            GameObject[] stores = GameObject.FindGameObjectsWithTag("Store");
            
            if (stores.Length == 0)
            {
                Debug.LogError("Could not find any objects with the `Store` tag for enemies to navigate to!", this);
                Destroy(this);  // If no store was found, this is useless
            }

            if (stores.Length > 1)
            {
                Debug.LogError("Found multiple objects with the `Store` tag! This may be confusing when enemies have to navigate to them", this);
            }

            _storeObject = stores[0];  // Set to first instance of store (ideally only one anyways)
        }

        /// <summary>
        /// Should be called when we want this object to start navigating towards
        /// the store object. All other sources of movement should be disabled when
        /// calling this.
        /// 
        /// Deletes this sprite when it is close enough to the store.
        /// </summary>
        public void TrackStore()
        {
            _path.enabled = true;
            _destinationSetter.target = _storeObject.transform;
            StartCoroutine(DeleteWhenCloseCoroutine());
        }

        /// <summary>
        /// When this object is close to the store, delete it.
        /// (Like the enemy has entered the store)
        /// </summary>
        private IEnumerator DeleteWhenCloseCoroutine()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Vector2.Distance(transform.position, _storeObject.transform.position) < DIST_FROM_STORE_UNTIL_DISAPPEAR);
            Destroy(gameObject);
        }

    }
}
