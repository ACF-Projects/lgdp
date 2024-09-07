using System.Collections.Generic;
using UnityEngine;
using RushHour.Data;
using DG.Tweening;

namespace RushHour
{
    public abstract class TowerHandler : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] protected Transform Visual;
        [SerializeField] protected Transform EffectPreview;
        [SerializeField] protected CircleCollider2D EffectTrigger;

        [HideInInspector] public bool IsActivated = false;  // No tower logic renders if `false`

        private const float EFFECT_ALPHA_ON_HOVER = 0.3f;  // Opacity of effect circle when shown

        /// <summary>
        /// Initializes this tower's data.
        /// 
        /// Also logs this unit's salary into Globals.
        /// </summary>
        public void Init(TowerData towerData)
        {
            Visual.GetComponent<SpriteRenderer>().sprite = towerData.TowerSprite;
            Visual.localScale = towerData.SpriteScale;
            float spriteDiameter = EffectPreview.GetComponent<SpriteRenderer>().bounds.size.x; // Assuming the sprite is a circle
            float scaleFactor = towerData.EffectRadius * 2f / spriteDiameter;
            EffectPreview.localScale = new Vector3(scaleFactor, scaleFactor);
            EffectTrigger.radius = towerData.EffectRadius;
        }
   
        /// <summary>
        /// Shows the circle around this unit detailing its effect radius.
        /// </summary>
        public void ShowEffectRadius()
        {
            SpriteRenderer previewRenderer = EffectPreview.GetComponent<SpriteRenderer>();
            DOTween.To(() => previewRenderer.color.a, 
                x => previewRenderer.color = new Color(previewRenderer.color.r, previewRenderer.color.g, previewRenderer.color.b, x), 
                EFFECT_ALPHA_ON_HOVER, 0.1f).SetEase(Ease.OutSine);
        }

        /// <summary>
        /// Hides the circle around this unit detailing its effect radius.
        /// </summary>
        public void HideEffectRadius()
        {
            SpriteRenderer previewRenderer = EffectPreview.GetComponent<SpriteRenderer>();
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
            EffectPreview.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, EFFECT_ALPHA_ON_HOVER);
        }

    }
}
