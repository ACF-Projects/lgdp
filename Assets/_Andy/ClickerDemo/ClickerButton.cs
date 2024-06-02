using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LGDP.Game;

namespace LGDP.UI
{
    [RequireComponent(typeof(Button))]
    public class ClickerButton : MonoBehaviour
    {
        private Button clicker;

        private void Awake()
        {
            clicker = GetComponent<Button>();
        }

        private void OnEnable()
        {
            clicker.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            clicker.onClick.RemoveAllListeners();
        }

        private void Click()
        {
            GameController.instance.AddMoney();
        }
    }
}
