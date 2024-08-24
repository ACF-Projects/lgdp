using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LGDP.TowerDefense.Lucas.POC
{
    public class EnemyPOCAnimLucas : MonoBehaviour
    {
        [SerializeField] private EnemyPOCLucas enemy;
        [SerializeField] private Image hpBar;

        private void OnEnable()
        {
            EnemyPOCLucas.OnEnemyHit += EnemyPOC_OnEnemyHit;
        }

        private void EnemyPOC_OnEnemyHit(EnemyPOCLucas enemy)
        {
            if (this.enemy != enemy || hpBar == null) return;

            DOTween.To(() => hpBar.fillAmount, x => hpBar.fillAmount = x, enemy.CurrentHealth / enemy.EnemyData.Health, 0.2f).SetEase(Ease.OutSine);
        }

        private void OnDisable()
        {
            EnemyPOCLucas.OnEnemyHit -= EnemyPOC_OnEnemyHit;
        }
    }
}
