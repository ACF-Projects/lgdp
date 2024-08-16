using Cinemachine;
using RushHour.InputHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RushHour
{
    public class CameraPanning : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera vCam;

        private Transform followTarget;

        [Header("Settings")]
        [SerializeField] private float panSpeed;
        

        private void Awake()
        {
            followTarget = vCam.Follow;
        }

        private void OnEnable()
        {
            MouseReceiver.OnMouseMoved += TryPanCamera;
        }

        private void OnDisable()
        {
            MouseReceiver.OnMouseMoved -= TryPanCamera;
        }

        private void TryPanCamera(Vector2 pos)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            print(pos);
        }

        
    }
}
