using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class TowerEntity : MonoBehaviour, IClickableEntity
    {
        public void Interact()
        {
            ContextManager.instance.ChangeContext(ContextType.Tower, this);
        }
    }
}
