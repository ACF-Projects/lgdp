using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour
{
    public class EnemyAnim : MonoBehaviour
    {
        [SerializeField] private EnemyHandler enemy;
        [SerializeField] private CanvasGroup healthDisplay;
        [SerializeField] private Image hpBar;

        private void OnEnable()
        {
            EnemyHandler.OnEnemyHit += OnEnemyHit;
            EnemyHandler.OnEnemyKilled += OnEnemyKilled;
        }

        private void OnEnemyKilled(EnemyHandler enemy)
        {
            if (this.enemy != enemy) return;
            healthDisplay.DOFade(0f, 0.5f).SetEase(Ease.OutSine);
        }

        private void OnEnemyHit(EnemyHandler enemy)
        {
            if (this.enemy != enemy || hpBar == null) return;
            float percentage = enemy.CurrentHealth / enemy.EnemyData.Health;
            DOTween.To(() => hpBar.fillAmount, x => hpBar.fillAmount = x, percentage, 0.1f).SetEase(Ease.InOutQuad);
        }

        private void OnDisable()
        {
            EnemyHandler.OnEnemyHit -= OnEnemyHit;
            EnemyHandler.OnEnemyKilled -= OnEnemyKilled;
        }
    }
}
