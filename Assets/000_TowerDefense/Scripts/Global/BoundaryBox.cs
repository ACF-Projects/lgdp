using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class BoundaryBox : MonoBehaviour
    {
        [SerializeField, ReadOnly] private EdgeCollider2D edgeCollider;
        [SerializeField] private Vector2 boxSize;

#if UNITY_EDITOR
        private void OnValidate()
        {
            edgeCollider = GetComponent<EdgeCollider2D>();
        }

        [Button("Create Boundary")]
        private void CreateBoundary()
        {
            Vector2 pos = transform.position;

            float halfLength = boxSize.x / 2f;
            float halfHeight = boxSize.y / 2f;

            List<Vector2> corners = new List<Vector2>()
            {
                pos + new Vector2(-halfLength, halfHeight),
                pos + new Vector2(halfLength, halfHeight),
                pos + new Vector2(halfLength, -halfHeight),
                pos + new Vector2(-halfLength, -halfHeight),
                pos + new Vector2(-halfLength, halfHeight),
            };

            edgeCollider.SetPoints(corners);
        }
#endif
    }
}
