using RushHour.UserInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class InfoBar : ContextElement
    {
        [SerializeField] private SlideUI slider;

        protected override void ProcessContext(bool passedCheck)
        {
            if (passedCheck)
            {
                slider.SlideIn();
            }
            else
            {
                slider.SlideOut();
            }
        }
    }
}
