using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public abstract class ContextElement : MonoBehaviour
    {
        [SerializeField] private ContextType allowedTypes;

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
           ProcessContext(allowedTypes.HasFlag(contextType));
        }

        protected abstract void ProcessContext(bool passedCheck);
    }
}
