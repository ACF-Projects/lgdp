using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower
{
    public class TowerEntity : TowerComponent, IClickableEntity
    {
        public void Interact()
        {
            ContextManager.instance.ChangeContext(ContextType.Tower, TowerHandler);
        }
    }
}
