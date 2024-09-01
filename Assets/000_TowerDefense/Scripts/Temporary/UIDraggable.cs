using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RushHour
{
    public class UIDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        [Header("Object Assignments")]
        [SerializeField] private Transform _transformToDrag;
        [SerializeField] private CanvasGroup _canvasGroup;  // Needed to disable/reenable raycasts

        public Action<Vector3> OnStartDrag;  // Parameter is position of this UI object when drag begins
        public Action<Vector3> OnBeingDragged;  // Parameter is position of this UI object while being dragged
        public Action<Vector3> OnDropped;  // Parameter is position of this UI object when dropped

        private Vector3 _originalPosition;
        private Vector2 _originalSizeDelta;

        private bool _isDragging = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            _originalPosition = _transformToDrag.localPosition;
            _originalSizeDelta = _transformToDrag.GetComponent<RectTransform>().sizeDelta;
            _canvasGroup.blocksRaycasts = false;  // Disable raycasts temporarily to check for UI underneath
            OnStartDrag?.Invoke(_transformToDrag.position);
        }

        private void Update()
        {
            if (!_isDragging) { return; }
            _transformToDrag.position = Input.mousePosition;  // Follow the mouse around
            OnBeingDragged?.Invoke(_transformToDrag.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            OnDropped?.Invoke(_transformToDrag.position);
            _canvasGroup.blocksRaycasts = true;  // Reenable raycasts
            _transformToDrag.localPosition = _originalPosition;
            _transformToDrag.GetComponent<RectTransform>().sizeDelta = _originalSizeDelta;
        }
    }
}
