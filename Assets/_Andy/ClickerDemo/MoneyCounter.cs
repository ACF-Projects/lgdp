using LGDP.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LGDP.UI
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;
        private void OnEnable()
        {
            GameController.instance.Money_OnChange += Money_OnChange;
            UpdateCounter();
        }

        private void OnDisable()
        {
            GameController.instance.Money_OnChange -= Money_OnChange;
        }

        private void Money_OnChange(int prev, int next)
        {
            counterText.text = next.ToString();
        }

        private void UpdateCounter()
        {
            counterText.text = GameController.instance.money.ToString();
        }
    }
}
