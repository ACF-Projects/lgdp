using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.Movement
{
    public class TopDownMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;

        [Header("Settings")]
        [SerializeField] private float moveSpeed;

        private Vector2 moveInput;
        private void Update()
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }
}
