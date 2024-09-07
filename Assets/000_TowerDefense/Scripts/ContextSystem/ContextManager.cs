using RushHour.InputHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RushHour
{
    [Flags]
    public enum ContextType
    {
        None = 1 << 0,
        Shop = 1 << 1,
        Tower = 1 << 2,

    }

    public class ContextManager : MonoBehaviour
    {
        public static ContextManager instance;

        public static event EventHandler<ContextType> OnContextUpdated;

        [HideInInspector] public ContextType currentContext;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            // Default context = None
            ChangeContext(ContextType.None);
        }

        private void OnEnable()
        {
            MouseReceiver.OnLeftClicked += CheckContext;
        }

        private void OnDisable()
        {
            MouseReceiver.OnLeftClicked -= CheckContext;
        }

        public void ChangeContext(ContextType newType = ContextType.None, object obj = null)
        {
            currentContext = newType;
            OnContextUpdated?.Invoke(obj, currentContext);
        }

        private void CheckContext()
        {
            if (MouseReceiver.instance.isPointerOverGameObject)
            {
                // Is UI Element
            }
            else
            {
                // Check for actual object
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Camera")));

                if (hit.collider == null)
                {
                    // Hit Nothing
                    ChangeContext(ContextType.None);
                    return;
                }

                if(hit.collider.TryGetComponent<IClickableEntity>(out var entity))
                {
                    entity.Interact();
                }
                else
                {
                    // Hit Nothing
                    ChangeContext(ContextType.None);
                    return;
                }
            }
        }

    }
}
