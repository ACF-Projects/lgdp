using DG.Tweening;
using LGDP.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.Interaction
{
    public class RoomTransition : MonoBehaviour, IInteractable
    {
        public InteractionType InteractionType { get; set; }

        public void Activate()
        {
            Camera cam = Camera.main;
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.aspect * halfHeight;

            cam.transform.DOMoveX(cam.transform.position.x + halfWidth * 2f, 0.5f);
        }
    }
}
