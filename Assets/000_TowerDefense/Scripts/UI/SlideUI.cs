using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.UserInterface
{
    public class SlideUI : MonoBehaviour
    {
        [SerializeField, ReadOnly] private RectTransform rectTransform;

        [SerializeField] private bool horizontal;
        [SerializeField] private float inPosition;
        [SerializeField] private float outPosition;

        [SerializeField] private float duration;
        [SerializeField] private Ease easeType = Ease.Linear;

        private float Position
        {
            get
            {
                return horizontal ? rectTransform.anchoredPosition.x : rectTransform.anchoredPosition.y;
            }
            set
            {
                rectTransform.anchoredPosition = new(horizontal ? value : rectTransform.anchoredPosition.x, horizontal ? rectTransform.anchoredPosition.y : value);
            }
        }

        public void SlideIn()
        {
            DOVirtual.Float(Position, inPosition, duration, v => Position = v).SetEase(easeType);
        }

        public void SlideOut()
        {
            DOVirtual.Float(Position, outPosition, duration, v => Position = v).SetEase(easeType);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        [Button("Set InPosition")]
        private void SetInPosition()
        {
            inPosition = Position;
        }

        [Button("Set OutPosition")]
        private void SetOutPosition()
        {
            outPosition = Position;
        }

#endif
        
        [Button("Reset Position")]
        private void ResetPosition()
        {
            Position = outPosition;
        }
    }
}
