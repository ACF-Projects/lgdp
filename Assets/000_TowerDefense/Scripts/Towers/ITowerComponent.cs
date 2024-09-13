using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower
{
    public interface ITowerComponent
    {
        TowerHandler TowerHandler { get; set; }

        void Init();
    }
}
