using DG.Tweening;
using RushHour.CameraControls;
using RushHour.Tower;
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
        [SerializeField] private GameObject _uiHireButtonArrow;
        [SerializeField] private Button _uiHireButton;
        [SerializeField] private GameObject _uiDragUnitParent;
        [SerializeField] private GameObject _uiMoneyParent;
        [SerializeField] private GameObject _uiTimerParent;
        [SerializeField] private GameObject _uiQuotaParent;
        [SerializeField] private GameObject _welcomePopup;
        [SerializeField] private Button _welcomePopupButton;
        [SerializeField] private GameObject _uiCameraPanGuide;
        [SerializeField] private GameObject _enemyIntroPopup;
        [SerializeField] private GameObject _blockedUnitDropMask;  // To force the player to put a unit in one spot
        [SerializeField] private GameObject _salaryPopup;
        [SerializeField] private GameObject _quotaIntroPopup;
        [SerializeField] private CameraPanning _cameraController;  // To pan the camera
        [SerializeField] private Transform _customerIntroCameraTransform;  // Camera will go here during customer intro
        [SerializeField] private Transform _moneyIntroCameraTransform;  // Camera will go here during money earned intro
        [SerializeField] private GameObject _moveUnitIntroPopup;
        [SerializeField] private Transform _moveUnitIntroCameraTransform;
        [SerializeField] private GameObject _blockedUnitMoveDropMask;  // To force the player to put a unit in one spot

        private Vector3 _storedCamPosition;  // Stored camera position, check to see how much camera moved

        private const float DIST_TO_UNPAUSE_TUTORIAL = 3;  // Distance (in unity units) needed to move until camera movement portion of tutorial is done

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
            _salaryPopup.SetActive(false);
            _blockedUnitMoveDropMask.SetActive(false);
            _uiHireButtonArrow.SetActive(false);
            _moveUnitIntroPopup.SetActive(false);
            _uiDragUnitParent.SetActive(false);
            EnemySpawner.CanSpawnEnemies = false;
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
                TimerTicker.CanTick = false;  // Freeze all timers initially for camera movement part
                _uiMoneyParent.SetActive(false);
                _uiCameraPanGuide.SetActive(true);
                _storedCamPosition = Camera.main.transform.position;
                StartCoroutine(UnfreezeTimersAfterCameraMovementCoroutine());
            }
            // Then, let player see enemy and place unit
            if (secs == 9)
            {
                Time.timeScale = 0;
                _enemyIntroPopup.SetActive(true);
                _uiHireButtonParent.SetActive(true);
                _uiHireButtonArrow.SetActive(true);
                _blockedUnitDropMask.SetActive(true);
                _uiMoneyParent.SetActive(true);
                _uiHireButton.onClick.AddListener(TutorialManager_OnHireButtonClicked);
                ContextManager.OnContextUpdated += TutorialManager_OnContextChange;
                MoveCameraTo(_customerIntroCameraTransform.position);
                // Unpause after tower is placed
                TowerMove.OnTowerDropped += TutorialManager_OnTowerBought;
            }
            // Then, wait until the enemy has been converted
            if (secs == 10)
            {
                TowerMove.OnTowerDropped -= TutorialManager_OnTowerBought;
                _uiHireButton.onClick.RemoveListener(TutorialManager_OnHireButtonClicked);
                ContextManager.OnContextUpdated -= TutorialManager_OnContextChange;
            }
            // Then, unsubscribe after enemy converted
            if (secs == 11)
            {
                BattleManager.Instance.OnMoneyChanged += TutorialManager_OnGainedMoney;
            }
            if (secs == 26)
            {
                BattleManager.Instance.OnMoneyChanged -= TutorialManager_OnGainedMoney;
                _quotaIntroPopup.SetActive(false);
                _moveUnitIntroPopup.SetActive(true);
                _blockedUnitMoveDropMask.SetActive(true);
                Time.timeScale = 0;
                FindObjectOfType<TowerMove>().IsGrabbable = true;  // Allow player to move unit now
                MoveCameraTo(_moveUnitIntroCameraTransform.position);
                TowerMove.OnTowerDropped += TutorialManager_OnTowerMoved;
            }
            if (secs == 31)
            {
                FindObjectOfType<TowerEntity>().CanInteract = true;
                TowerMove.OnTowerDropped -= TutorialManager_OnTowerMoved;
            }
        }

        private void ToggleTimeScale()
        {
            Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        }

        private void TutorialManager_OnHireButtonClicked()
        {
            // Toggle whether animation is visible
            _uiDragUnitParent.SetActive(true);
        }
        private void TutorialManager_OnContextChange(object obj, ContextType contextType)
        {
            // Toggle whether animation is visible
            _uiDragUnitParent.SetActive(false);
        }

        private void TutorialManager_OnTowerBought(object sender, bool isValid)
        {
            if (isValid)
            {
                Time.timeScale = 1;
                _enemyIntroPopup.SetActive(false);
                _blockedUnitDropMask.SetActive(false);
                _salaryPopup.SetActive(true);
                _uiHireButtonArrow.SetActive(false);
                _uiDragUnitParent.SetActive(false);
                FindObjectOfType<TowerEntity>().CanInteract = false;
                FindObjectOfType<TowerMove>().IsGrabbable = false;
            }
        }

        private void TutorialManager_OnTowerMoved(object sender, bool isValid)
        {
            if (isValid)
            {
                Time.timeScale = 1;
                _moveUnitIntroPopup.SetActive(false);
                _blockedUnitMoveDropMask.SetActive(false);
            }
        }

        private void TutorialManager_OnGainedMoney()
        {
            _salaryPopup.SetActive(false);
            _uiQuotaParent.SetActive(true);
            _quotaIntroPopup.SetActive(true);
            MoveCameraTo(_moneyIntroCameraTransform.position);
        }

        private IEnumerator UnfreezeTimersAfterCameraMovementCoroutine()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Vector2.Distance(Camera.main.transform.position, _storedCamPosition) > DIST_TO_UNPAUSE_TUTORIAL);
            TimerTicker.CanTick = true;
            EnemySpawner.CanSpawnEnemies = true;
            _uiTimerParent.SetActive(true);
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
