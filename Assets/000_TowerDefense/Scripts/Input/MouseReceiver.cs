using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace RushHour.InputHandling
{
    public class MouseReceiver : MonoBehaviour
    {
        public static MouseReceiver instance;

        public static event Action OnLeftClicked;
        public static event Action OnLeftReleased;
        public static event Action OnRightClicked;
        public static event Action<Vector2> OnMouseMoved;

        public bool isPointerOverGameObject;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnLeftClicked?.Invoke();
            }
            else if(context.canceled)
            {
                OnLeftReleased?.Invoke();
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnRightClicked?.Invoke();
            }
        }

        public void OnPan(InputAction.CallbackContext context)
        {
            if (context.canceled) return;
            OnMouseMoved?.Invoke(context.ReadValue<Vector2>());
        }

        private void Update()
        {
            isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
        }
    }
}
