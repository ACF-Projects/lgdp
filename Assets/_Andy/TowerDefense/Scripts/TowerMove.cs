using RushHour.Data;
using RushHour.InputHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RushHour
{
    public class TowerMove : MonoBehaviour
    {
        [SerializeField] private TowerData data;
        [SerializeField] private Collider2D towerCollider;
        [SerializeField] private LayerMask blockedLayers;

        private bool dragging;
        private Vector3 originalPosition;

        private void OnEnable()
        {
            MouseReceiver.OnLeftClicked += MouseReceiver_OnLeftClicked;
            MouseReceiver.OnLeftReleased += MouseReceiver_OnLeftReleased;
            MouseReceiver.OnMouseMoved += MouseReceiver_OnMouseMoved;
        }

        private void OnDisable()
        {
            MouseReceiver.OnLeftClicked -= MouseReceiver_OnLeftClicked;
            MouseReceiver.OnLeftReleased -= MouseReceiver_OnLeftReleased;
            MouseReceiver.OnMouseMoved -= MouseReceiver_OnMouseMoved;
        }

        private void MouseReceiver_OnLeftClicked()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Camera")));
            if (hit.collider != towerCollider) return;
            dragging = true;
            originalPosition = transform.position;
            gameObject.layer = 0;
        }

        private void MouseReceiver_OnLeftReleased()
        {
            dragging = false;
            var col = Physics2D.OverlapCircle(transform.position, data.SpriteRadius, blockedLayers);
            if (col != null) transform.position = originalPosition;
            gameObject.layer = LayerMask.NameToLayer("Blocked");
        }

        private void MouseReceiver_OnMouseMoved(Vector2 pos)
        {
            if (dragging)
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
                transform.position = new Vector3(worldPos.x, worldPos.y, 0);
            }
        }
    }
}
