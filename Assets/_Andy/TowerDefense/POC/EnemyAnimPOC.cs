using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour.POC
{
    public class EnemyAnimPOC : MonoBehaviour
    {
        [SerializeField] private EnemyPOC enemy;
        [SerializeField] private Image hpBar;

        private void OnEnable()
        {
            EnemyPOC.OnEnemyHit += EnemyPOC_OnEnemyHit;
        }

        private void EnemyPOC_OnEnemyHit(EnemyPOC enemy)
        {
            if (this.enemy != enemy || hpBar == null) return;

            DOTween.To(() => hpBar.fillAmount, x => hpBar.fillAmount = x, enemy.CurrentHealth / enemy.EnemyData.Health, 0.2f).SetEase(Ease.OutSine);
        }

        private void OnDisable()
        {
            EnemyPOC.OnEnemyHit -= EnemyPOC_OnEnemyHit;
        }
    }
}
