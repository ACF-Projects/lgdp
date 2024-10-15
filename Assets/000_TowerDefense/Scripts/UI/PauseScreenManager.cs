using DG.Tweening;
using NaughtyAttributes;
using RushHour.InputHandling;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour.UserInterface
{
    public class PauseScreenManager : MonoBehaviour
    {
        public enum PauseScreen
        {
            Main,
            Settings,
            QuitConfirm
        }

        [SerializeField] private CanvasGroup pauseScreen;
        [SerializeField, ReadOnly] private PauseScreen currentScreen;

        [SerializeField] private Image quitConfirmBackground;
        [SerializeField] private Transform quitConfirmPanel;

        [SerializeField] private Image controlsBackground;
        [SerializeField] private Transform controlsPanel;

        private bool showingPauseScreen;
        private float previousTimeScale;

        private void Awake()
        {
            currentScreen = PauseScreen.Main;

            pauseScreen.alpha = 0f;
            pauseScreen.blocksRaycasts = false;
            showingPauseScreen = false;

            if(quitConfirmBackground != null )
            {
                quitConfirmBackground.DOFade(0f, 0.01f);
                quitConfirmBackground.raycastTarget = false;
                quitConfirmPanel.localScale = Vector3.zero;
            }

            if(controlsBackground != null)
            {
                controlsBackground.DOFade(0f, 0.01f);
                controlsBackground.raycastTarget = false;
                controlsPanel.localScale = Vector3.zero;
            }
            
        }

        private void OnEnable()
        {
            KeyboardReceiver.OnEscapePressed += OnEscapeKey;
        }

        private void OnDisable()
        {
            KeyboardReceiver.OnEscapePressed -= OnEscapeKey;
        }

        public void OnEscapeKey()
        {
            switch (currentScreen)
            {
                case PauseScreen.Main:
                    showingPauseScreen = !showingPauseScreen;
                    if (showingPauseScreen) previousTimeScale = Time.timeScale;
                    Time.timeScale = showingPauseScreen ? 0f : previousTimeScale;
                    float targetAlpha = showingPauseScreen ? 1f : 0f;
                    pauseScreen.DOFade(targetAlpha, 0.5f).SetEase(Ease.OutSine).SetUpdate(true);
                    pauseScreen.blocksRaycasts = showingPauseScreen;
                    break;
                case PauseScreen.Settings:
                    break;
                case PauseScreen.QuitConfirm:
                    break;
            }
        }



    }
}
