using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class TowerInfoDisplay : ContextElement
    {
        [SerializeField] private CanvasGroup towerInfoGroup;

        private TowerEntity towerEntity;

        private void Awake()
        {
            towerInfoGroup.alpha = 0f;
            towerInfoGroup.blocksRaycasts = false;
            towerEntity = null;
        }

        protected override void ProcessContext(object obj, ContextType contextType)
        {
            if(!allowedTypes.HasFlag(contextType))
            {
                towerInfoGroup.alpha = 0f;
                towerInfoGroup.blocksRaycasts = false;
                towerEntity = null;
            }
            else
            {
                towerInfoGroup.alpha = 1f;
                towerInfoGroup.blocksRaycasts = true;
                towerEntity = obj as TowerEntity;
            }
        }

        public void SellUnit()
        {
            Destroy(towerEntity.gameObject);
            ContextManager.instance.ChangeContext();
        }
    }
}
