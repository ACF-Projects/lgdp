using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RushHour
{
    public class Draggable : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler
    {
        public event Action<PointerEventData> OnDragged;
        public event Action<PointerEventData> OnDropped;
        public event Action<PointerEventData> OnPointerDowned;

        public void OnDrag(PointerEventData eventData)
        {
            OnDragged?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDowned?.Invoke(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnDropped?.Invoke(eventData);
        }
    }
}
