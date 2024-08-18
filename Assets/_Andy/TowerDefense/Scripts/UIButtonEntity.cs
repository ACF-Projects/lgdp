using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour
{
    [RequireComponent(typeof(Button))]
    public class UIButtonEntity : MonoBehaviour, IClickableEntity
    {
        [SerializeField] private ContextType context;

        [SerializeField, ReadOnly] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(Interact);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        public void Interact()
        {
            ContextManager.instance.ChangeContext(context, this);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            button = GetComponent<Button>();
        }
#endif
    }
}
