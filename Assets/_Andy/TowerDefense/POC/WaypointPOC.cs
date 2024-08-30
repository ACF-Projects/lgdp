using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.POC
{
    public class WaypointPOC : MonoBehaviour
    {
        [ReadOnly] public List<Transform> waypoints;

#if UNITY_EDITOR
        private void OnValidate()
        {
            waypoints = new();
            foreach (Transform child in transform)
            {
                waypoints.Add(child);
            }
        }
#endif
    }
}
