using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RushHour
{
    public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        [Header("Object Assignments")]
        [SerializeField] private Transform _transformToDrag;
        [SerializeField] private CanvasGroup _canvasGroup;  // Needed to disable/reenable raycasts

        public Action<Vector3> OnBeingDragged;  // Parameter is position of this UI object while being dragged
        public Action<Vector3> OnDropped;  // Parameter is position of this UI object when dropped

        private Vector3 originalPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = _transformToDrag.position;  // Store original position to reset after
            _canvasGroup.blocksRaycasts = false;  // Disable raycasts temporarily to check for UI underneath
        }

        public void OnDrag(PointerEventData eventData)
        {
            _transformToDrag.position = Input.mousePosition;  // Follow the mouse around
            OnBeingDragged?.Invoke(_transformToDrag.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDropped?.Invoke(_transformToDrag.position);
            _canvasGroup.blocksRaycasts = true;  // Reenable raycasts
            _transformToDrag.position = originalPosition;  // Return to original position
        }
    }
}
