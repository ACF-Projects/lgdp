using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public abstract class ContextElement : MonoBehaviour
    {
        [SerializeField] protected ContextType allowedTypes;

        private void OnEnable()
        {
            ContextManager.OnContextUpdated += OnContextChange;
        }

        private void OnDisable()
        {
            ContextManager.OnContextUpdated -= OnContextChange;
        }

        private void OnContextChange(object obj, ContextType contextType)
        {
           ProcessContext(obj, contextType);
        }

        protected abstract void ProcessContext(object obj, ContextType contextType);
    }
}
