using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RushHour
{
    public class TransitionManager : MonoBehaviour
    {

        public static bool HasFadedInFromPrevScene = true;

        private static TransitionManager _instance;
        public static TransitionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TransitionManager>();
                }
                return _instance;
            }
        }

        [Header("Object Assignments")]
        [SerializeField] private Animator _transitionAnimator;

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            if (HasFadedInFromPrevScene)
            {
                TransitionFadeOut();
            }
        }

        /// <summary>
        /// Given the name of a scene, plays the transition animation and goes to that
        /// scene.
        /// </summary>
        public void GoToScene(string sceneName)
        {
            TransitionAndCall(() =>
            {
                SceneManager.LoadScene(sceneName);
            });
        }

        /// <summary>
        /// Given code to invoke, invokes it after playing the transition animation.
        /// </summary>
        public void TransitionAndCall(Action codeToInvoke)
        {
            StartCoroutine(TransitionAndCallCoroutine(codeToInvoke));
        }

        private IEnumerator TransitionAndCallCoroutine(Action codeToInvoke)
        {
            TransitionFadeIn();
            // Wait until animation is done, then invoke code
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => _transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            codeToInvoke?.Invoke();
        }

        /// <summary>
        /// Makes the "screen filler" fade from transparent to opaque.
        /// </summary>
        private void TransitionFadeIn()
        {
            StartCoroutine(LerpAudio(1, 0));
            _transitionAnimator.Play("Show");
            HasFadedInFromPrevScene = true;
        }

        /// <summary>
        /// Makes the "screen filler" fade from opaque to transparent.
        /// </summary>
        private void TransitionFadeOut()
        {
            StartCoroutine(LerpAudio(0, 1));
            _transitionAnimator.Play("Hide");
            HasFadedInFromPrevScene = false;
        }

        private IEnumerator LerpAudio(float fromVolume, float toVolume)
        {
            float currTime = 0;
            float timeToWait = 0.2f;
            while (currTime < timeToWait)
            {
                currTime += Time.unscaledDeltaTime;
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.SetVolume(Mathf.Lerp(fromVolume, toVolume, currTime / timeToWait));
                }
                yield return null;
            }
        }

    }
}