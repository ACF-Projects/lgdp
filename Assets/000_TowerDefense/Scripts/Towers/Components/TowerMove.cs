using NaughtyAttributes;
using RushHour.Global;
using RushHour.InputHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

namespace RushHour.Tower.Components
{
    public class TowerMove : TowerComponent
    {
        [Header("References")]
        [SerializeField] private Collider2D towerCollider;

        [Header("Settings")]
        [SerializeField] private LayerMask blockedLayers;

        private bool dragging;
        private Vector3? originalPosition = null;
        private Vector2 mousePosition;

        public static event EventHandler OnTowerGrabbed;
        public static event EventHandler<bool> OnTowerDropped;
        public static event EventHandler OnTowerBought;

        private void Awake()
        {
            dragging = false;
        }

        public override void Init(TowerHandler handler)
        {
            base.Init(handler);

            MouseReceiver.OnLeftClicked += MouseReceiver_OnLeftClicked;
            MouseReceiver.OnLeftReleased += MouseReceiver_OnLeftReleased;
            MouseReceiver.OnMouseMoved += MouseReceiver_OnMouseMoved;

            dragging = true;
            originalPosition = null;
        }

        private void OnDestroy()
        {
            MouseReceiver.OnLeftClicked -= MouseReceiver_OnLeftClicked;
            MouseReceiver.OnLeftReleased -= MouseReceiver_OnLeftReleased;
            MouseReceiver.OnMouseMoved -= MouseReceiver_OnMouseMoved;
        }

        //private void OnEnable()
        //{
        //    MouseReceiver.OnLeftClicked += MouseReceiver_OnLeftClicked;
        //    MouseReceiver.OnLeftReleased += MouseReceiver_OnLeftReleased;
        //    MouseReceiver.OnMouseMoved += MouseReceiver_OnMouseMoved;
        //}

        //private void OnDisable()
        //{
        //    MouseReceiver.OnLeftClicked -= MouseReceiver_OnLeftClicked;
        //    MouseReceiver.OnLeftReleased -= MouseReceiver_OnLeftReleased;
        //    MouseReceiver.OnMouseMoved -= MouseReceiver_OnMouseMoved;
        //}

        private void MouseReceiver_OnLeftClicked()
        {
            if (MouseReceiver.instance.isPointerOverGameObject) return;
            Physics2D.queriesHitTriggers = false;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Camera")));
            Physics2D.queriesHitTriggers = true;
            if (hit.collider != towerCollider) return;
            
            dragging = true;
            originalPosition = TowerHandler.transform.position;
            towerCollider.gameObject.layer = 0;
            OnTowerGrabbed?.Invoke(TowerHandler, EventArgs.Empty);
        }

        private void MouseReceiver_OnLeftReleased()
        {
            if (!dragging) return;
            dragging = false;
            var col = Physics2D.OverlapCircle(TowerHandler.transform.position, TowerHandler.TowerData.SpriteRadius, blockedLayers);
            bool valid = col == null;

            OnTowerDropped?.Invoke(TowerHandler, valid);

            if (valid)
            {

            }
            else
            {
                AudioManager.Instance.PlayOneShot(SoundEffect.InvalidPlacement);
            }

            if (originalPosition is Vector3 position)
            {
                if (!valid)
                {
                    TowerHandler.transform.position = position;
                }
                towerCollider.gameObject.layer = LayerMask.NameToLayer("Blocked");
            }
            else
            {
                // if tower has never been placed or not enough funds, place tower if possible
                if (!valid || BattleManager.Instance.Money < TowerHandler.TowerData.Cost)
                {
                    Destroy(TowerHandler.gameObject);
                    return;
                }
                 
                towerCollider.gameObject.layer = LayerMask.NameToLayer("Blocked");
                originalPosition = TowerHandler.transform.position;

                OnTowerBought?.Invoke(TowerHandler, EventArgs.Empty);
            }

        }

        private void MouseReceiver_OnMouseMoved(Vector2 pos)
        {
            mousePosition = pos;
        }

        private void Update()
        {
            if (dragging)
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
                TowerHandler.transform.position = new Vector3(worldPos.x, worldPos.y, 0);

                var col = Physics2D.OverlapCircle(TowerHandler.transform.position, TowerHandler.TowerData.SpriteRadius, blockedLayers);
                bool valid = col == null;

                TowerHandler.GetTowerComponent<TowerRadius>().TintEffectRadius(valid ? Constants.VALID_PLACEMENT_COLOR : Constants.INVALID_PLACEMENT_COLOR);
            }
        }
    }
}
