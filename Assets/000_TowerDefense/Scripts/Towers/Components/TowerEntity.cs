using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower
{
    public class TowerEntity : TowerComponent, IClickableEntity
    {
        public bool CanInteract = true;  // If false, clicking tower won't open UI

        public void Interact()
        {
            if (CanInteract)
            {
                ContextManager.instance.ChangeContext(ContextType.Tower, TowerHandler);
            }
        }
    }
}
