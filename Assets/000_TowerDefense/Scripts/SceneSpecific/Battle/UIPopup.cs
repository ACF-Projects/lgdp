using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class UIPopup : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private CanvasGroup _canvasGroup;

        /// <summary>
        /// Hides this popup.
        /// </summary>
        public void OnClick_PopupHide()
        {
            StartCoroutine(PopupHideCoroutine());
        }

        private IEnumerator PopupHideCoroutine()
        {
            float currTime = 0;
            float timeToHide = 0.5f;
            while (currTime < timeToHide)
            {
                currTime += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1, 0, currTime / timeToHide);
                yield return null;
            }
            gameObject.SetActive(false);  // Disable whole object afterwards
        }

    }
}
