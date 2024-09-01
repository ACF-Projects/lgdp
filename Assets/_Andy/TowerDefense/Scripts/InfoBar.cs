using RushHour.Data;
using RushHour.UserInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class InfoBar : ContextElement
    {
        [SerializeField] private SlideUI slider;

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

            var towerData = obj as TowerData;
        }
    }
}
