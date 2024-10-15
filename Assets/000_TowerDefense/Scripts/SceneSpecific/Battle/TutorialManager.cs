using DG.Tweening;
using RushHour.CameraControls;
using RushHour.Tower.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour
{
    public class TutorialManager : MonoBehaviour
    {

        [Header("Tutorial Assignments")]
        [SerializeField] private GameObject _uiHireButtonParent;
        [SerializeField] private GameObject _uiMoneyParent;
        [SerializeField] private GameObject _uiTimerParent;
        [SerializeField] private GameObject _uiQuotaParent;
        [SerializeField] private GameObject _welcomePopup;
        [SerializeField] private Button _welcomePopupButton;
        [SerializeField] private GameObject _uiCameraPanGuide;
        [SerializeField] private GameObject _enemyIntroPopup;
        [SerializeField] private GameObject _blockedUnitDropMask;  // To force the player to put a unit in one spot
        [SerializeField] private GameObject _quotaIntroPopup;
        [SerializeField] private CameraPanning _cameraController;  // To pan the camera
        [SerializeField] private Transform _customerIntroCameraTransform;  // Camera will go here during customer intro
        [SerializeField] private Transform _moneyIntroCameraTransform;  // Camera will go here during money earned intro

        private Vector3 _storedCamPosition;  // Stored camera position, check to see how much camera moved

        private void Awake()
        {
            Time.timeScale = 0;
            _uiMoneyParent.SetActive(false);
            _uiTimerParent.SetActive(false);
            _welcomePopup.SetActive(true);
            _uiHireButtonParent.SetActive(false);
            _uiQuotaParent.SetActive(false);
            _blockedUnitDropMask.SetActive(false);
            _quotaIntroPopup.SetActive(false);
        }

        /// <summary>
        /// Register events to show/hide popups at correct times.
        /// </summary>
        private void OnEnable()
        {
            _welcomePopupButton.onClick.AddListener(ToggleTimeScale);
            BattleManager.Instance.OnTimerChanged += TutorialManager_OnTimerChanged;
        }

        /// <summary>
        /// Unregister all events made in OnEnable().
        /// </summary>
        private void OnDisable()
        {
            _welcomePopupButton.onClick.RemoveListener(ToggleTimeScale);
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnTimerChanged -= TutorialManager_OnTimerChanged;
            }
        }

        private void TutorialManager_OnTimerChanged(int secs)
        {
            // First, let player learn how to pan the camera
            if (secs == 1)
            {
                _uiMoneyParent.SetActive(false);
                _uiTimerParent.SetActive(false);
                _uiCameraPanGuide.SetActive(true);
                _storedCamPosition = Camera.main.transform.position;
                Time.timeScale = 0;
                StartCoroutine(UnfreezeTimeAfterCameraMovementCoroutine());
            }
            // Then, let player see enemy and place unit
            if (secs == 9)
            {
                Time.timeScale = 0;
                _enemyIntroPopup.SetActive(true);
                _uiHireButtonParent.SetActive(true);
                _blockedUnitDropMask.SetActive(true);
                _uiMoneyParent.SetActive(true);
                MoveCameraTo(_customerIntroCameraTransform.position);
                // Unpause after tower is placed
                TowerMove.OnTowerDropped += TutorialManager_OnTowerBought;
            }
            // Then, wait until the enemy has been converted
            if (secs == 10)
            {
                TowerMove.OnTowerDropped -= TutorialManager_OnTowerBought;
                EnemyHandler.OnEnemyKilled += TutorialManager_OnEnemyKilled;
            }
            // Then, unsubscribe after enemy converted
            if (secs == 11)
            {
                BattleManager.Instance.OnMoneyChanged += TutorialManager_OnGainedMoney;
                EnemyHandler.OnEnemyKilled -= TutorialManager_OnEnemyKilled;
            }
            if (secs == 25)
            {
                BattleManager.Instance.OnMoneyChanged -= TutorialManager_OnGainedMoney;
                _quotaIntroPopup.SetActive(false);
            }
        }

        private void ToggleTimeScale()
        {
            Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        }

        private void TutorialManager_OnTowerBought(object sender, bool isValid)
        {
            if (isValid)
            {
                Time.timeScale = 1;
                _enemyIntroPopup.SetActive(false);
                _blockedUnitDropMask.SetActive(false);
            }
        }

        private void TutorialManager_OnEnemyKilled(EnemyHandler e)
        {
            Time.timeScale = 1;
        }

        private void TutorialManager_OnGainedMoney()
        {
            _uiQuotaParent.SetActive(true);
            _quotaIntroPopup.SetActive(true);
            MoveCameraTo(_moneyIntroCameraTransform.position);
        }

        private IEnumerator UnfreezeTimeAfterCameraMovementCoroutine()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Vector2.Distance(Camera.main.transform.position, _storedCamPosition) > 3);
            Time.timeScale = 1;
            _uiCameraPanGuide.SetActive(false);
        }

        /// <summary>
        /// Pans the camera to a specific position because of the target of
        /// the Cinemachine.
        /// </summary>
        /// <param name="position"></param>
        private async void MoveCameraTo(Vector3 position)
        {
            _cameraController.EnablePanning = false;
            await _cameraController.transform.DOMove(position, 1f).SetEase(Ease.InOutSine).SetUpdate(true).AsyncWaitForCompletion();
            _cameraController.EnablePanning = true;
        }

    }
}
