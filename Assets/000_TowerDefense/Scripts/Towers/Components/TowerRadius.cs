using DG.Tweening;
using RushHour.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.Tower.Components
{
    public class TowerRadius : TowerComponent
    {

        [Header("References")]
        [SerializeField] private Transform effectPreview;
        [SerializeField] private CircleCollider2D effectTrigger;
        
        public override void Init(TowerHandler handler)
        {
            base.Init(handler);

            float spriteDiameter = effectPreview.GetComponent<SpriteRenderer>().bounds.size.x; // Assuming the sprite is a circle
            float scaleFactor = TowerHandler.TowerData.EffectRadius * 2f / spriteDiameter;
            effectPreview.localScale = new Vector3(scaleFactor, scaleFactor);
            effectTrigger.radius = TowerHandler.TowerData.EffectRadius;
        }

        /// <summary>
        /// Shows the circle around this unit detailing its effect radius.
        /// </summary>
        public void ShowEffectRadius()
        {
            SpriteRenderer previewRenderer = effectPreview.GetComponent<SpriteRenderer>();
            DOTween.To(() => previewRenderer.color.a,
                x => previewRenderer.color = new Color(previewRenderer.color.r, previewRenderer.color.g, previewRenderer.color.b, x),
                Constants.EFFECT_ALPHA_ON_HOVER, 0.1f).SetEase(Ease.OutSine);
        }

        /// <summary>
        /// Hides the circle around this unit detailing its effect radius.
        /// </summary>
        public void HideEffectRadius()
        {
            SpriteRenderer previewRenderer = effectPreview.GetComponent<SpriteRenderer>();
            DOTween.To(() => previewRenderer.color.a,
                x => previewRenderer.color = new Color(previewRenderer.color.r, previewRenderer.color.g, previewRenderer.color.b, x),
                0, 0.1f).SetEase(Ease.OutSine);
        }

        /// <summary>
        /// Given a color, tints the effect radius to view as that color.
        /// Maintains the same alpha value defined by the alpha constant.
        /// </summary>
        public void TintEffectRadius(Color c)
        {
            effectPreview.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, Constants.EFFECT_ALPHA_ON_HOVER);
        }
    }
}
