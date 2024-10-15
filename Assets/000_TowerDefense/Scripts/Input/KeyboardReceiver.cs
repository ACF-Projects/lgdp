using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RushHour.InputHandling
{
    public class KeyboardReceiver : MonoBehaviour
    {
        public static KeyboardReceiver instance;

        public static event Action OnEscapePressed;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnEscapePressed?.Invoke();
            }
        }
    }
}
