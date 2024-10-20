using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Global
{
    public static class Constants
    {
        public const float EFFECT_ALPHA_ON_HOVER = 0.3f; // Opacity of effect circle when shown
        public static readonly Color INVALID_PLACEMENT_COLOR = Color.red;
        public static readonly Color VALID_PLACEMENT_COLOR = Color.white;

        public static readonly LayerMask LAYERS_ALL_EXCEPT_CAMERA = ~(1 << LayerMask.NameToLayer("Camera"));

        public const int TIME_IN_DAY = 120;  // Number of seconds in a day until day ends
        public const int TIME_IN_HOUR = 20;  // How many seconds must pass for an in-game hour

        public const float WEAKNESS_DAMAGE_MULTIPLIER = 1.5f; // Enemies with a weakness to the tower will receive damage scaled by WEAKNESS_DAMAGE_MULTIPLIER 
        public const float RESISTANCE_DAMAGE_MULTIPLIER = 0.75f; // Enemies with a resistance to the tower will receive damage scaled by WEAKNESS_DAMAGE_MULTIPLIER
    }
}
