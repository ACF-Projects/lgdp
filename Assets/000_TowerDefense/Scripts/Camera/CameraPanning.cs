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

        private Rigidbody2D followTarget;

        [Header("Settings")]
        [SerializeField] private float panSpeed;
        [SerializeField] private float panBorderWidth;

        private Vector2 direction;

        private void Awake()
        {
            followTarget = vCam.Follow.GetComponent<Rigidbody2D>();
            direction = Vector2.zero;
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
            direction = Vector2.zero;

            if (EventSystem.current.IsPointerOverGameObject()) return;

            GetPanDirection(pos.x, pos.y);

            followTarget.velocity = direction * panSpeed;
        }

        private void GetPanDirection(float x, float y)
        {
            if(y >= Screen.height - panBorderWidth)
            {
                direction.y += 1;
            }
            else if(y <= panBorderWidth)
            {
                direction.y -= 1;
            }

            if(x >= Screen.width - panBorderWidth)
            {
                direction.x += 1;
            }
            else if(x <= panBorderWidth)
            {
                direction.x -= 1;
            }

        }
        
    }
}
