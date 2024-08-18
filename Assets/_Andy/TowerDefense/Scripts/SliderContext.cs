using NaughtyAttributes;
using RushHour.UserInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    [RequireComponent(typeof(SlideUI))]
    public class SliderContext : ContextElement
    {
        [SerializeField, ReadOnly] private SlideUI slider;

        protected override void ProcessContext(object obj, ContextType contextType)
        {
            bool passedCheck = allowedTypes.HasFlag(contextType);
            if (passedCheck)
            {
                slider.SlideIn();
            }
            else
            {
                slider.SlideOut();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            slider = GetComponent<SlideUI>();
        }
#endif
    }
}
