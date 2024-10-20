using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Data
{
    [Flags]
    public enum EnemyType
    {
        None = 0,
        Elderly = 1 << 0,
        Children = 1 << 1,

    }
}
