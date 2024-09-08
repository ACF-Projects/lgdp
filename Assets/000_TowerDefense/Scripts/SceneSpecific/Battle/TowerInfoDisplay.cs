using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour
{
    public class TowerInfoDisplay : ContextElement
    {
        [SerializeField] private CanvasGroup towerInfoGroup;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private TowerEntity towerEntity;

        private void Awake()
        {
            ResetDisplay();
        }

        protected override void ProcessContext(object obj, ContextType contextType)
        {
            if(!allowedTypes.HasFlag(contextType))
            {
                ResetDisplay();
            }
            else
            {
                towerInfoGroup.alpha = 1f;
                towerInfoGroup.blocksRaycasts = true;

                towerEntity = obj as TowerEntity;

                nameText.text = towerEntity.towerData.Name;
                descriptionText.text = towerEntity.towerData.Description;
            }
        }

        private void ResetDisplay()
        {
            towerInfoGroup.alpha = 0f;
            towerInfoGroup.blocksRaycasts = false;
            towerEntity = null;
            nameText.text = "";
            descriptionText.text = "";
        }

        public void SellUnit()
        {
            Destroy(towerEntity.gameObject);
            ContextManager.instance.ChangeContext();
        }
    }
}
