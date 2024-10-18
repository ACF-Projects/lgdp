using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class LevelAreaSection : MonoBehaviour
    {

        /// <summary>
        /// Name of the area. 
        /// Will update the text that labels the current section's name.
        /// </summary>
        public string AreaName;
        /// <summary>
        /// If this level section is navigable to.
        /// If true, shows up in the "slideshow" view. Else it doesn't.
        /// </summary>
        public bool IsNavigable = false;

    }
}
