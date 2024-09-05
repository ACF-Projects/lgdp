using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour
{
    public class EnemyAnim : MonoBehaviour
    {
        [SerializeField] private EnemyHandler enemy;
        [SerializeField] private Image hpBar;

        private void OnEnable()
        {
            EnemyHandler.OnEnemyHit += EnemyPOC_OnEnemyHit;
        }

        private void EnemyPOC_OnEnemyHit(EnemyHandler enemy)
        {
            if (this.enemy != enemy || hpBar == null) return;

            DOTween.To(() => hpBar.fillAmount, x => hpBar.fillAmount = x, enemy.CurrentHealth / enemy.EnemyData.Health, 0.2f).SetEase(Ease.OutSine);
        }

        private void OnDisable()
        {
            EnemyHandler.OnEnemyHit -= EnemyPOC_OnEnemyHit;
        }
    }
}
