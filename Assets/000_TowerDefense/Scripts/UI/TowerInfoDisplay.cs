using RushHour.Tower;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour.UserInterface
{
    public class TowerInfoDisplay : ContextElement
    {
        [SerializeField] private CanvasGroup towerInfoGroup;
        [SerializeField] private StatsDisplay statsDisplay;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private TowerHandler towerHandler;

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

                towerHandler = obj as TowerHandler;

                nameText.text = towerHandler.TowerData.Name;
                descriptionText.text = towerHandler.TowerData.Description;
            }
        }

        private void ResetDisplay()
        {
            towerInfoGroup.alpha = 0f;
            towerInfoGroup.blocksRaycasts = false;
            towerHandler = null;
            nameText.text = "";
            descriptionText.text = "";
        }

        // Temporary Function
        public void SellUnit()
        {
            Destroy(towerHandler.gameObject);
            ContextManager.instance.ChangeContext();
        }
    }
}
