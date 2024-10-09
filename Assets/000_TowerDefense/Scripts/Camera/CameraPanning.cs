using Cinemachine;
using RushHour.InputHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour.CameraControls
{
    public class CameraPanning : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera vCam;

        private Rigidbody2D followTarget;

        [Header("Settings")]
        [SerializeField] private float panSpeed;
        [SerializeField] private float panBorderWidth;
        
        public bool EnablePanning { get; set; }

        private Vector2 direction;

        private void Awake()
        {
            followTarget = vCam.Follow.GetComponent<Rigidbody2D>();
            direction = Vector2.zero;
            EnablePanning = true;
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
            if(!EnablePanning)
            {
                followTarget.velocity = Vector2.zero;
                return;
            }
            direction = Vector2.zero;

            if (MouseReceiver.instance.isPointerOverGameObject)
            {
                followTarget.velocity = Vector2.zero;
                return;
            }

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
