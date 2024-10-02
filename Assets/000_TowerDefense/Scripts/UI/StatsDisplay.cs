using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour.UserInterface
{
    [System.Serializable]
    public class StatSlot<T>
    {
        public TextMeshProUGUI nameField;
        public T valueField;
    }

    public class StatsDisplay : MonoBehaviour
    {
        //[SerializeField] private StatSlot[] slots;
    }
}
