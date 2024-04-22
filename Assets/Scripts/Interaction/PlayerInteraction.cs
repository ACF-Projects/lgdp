using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGDP.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IInteractable interactable))
            {
                if(interactable.InteractionType == InteractionType.Trigger) interactable.Activate();
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Collider2D other = Physics2D.OverlapCircle(transform.position, 0.5f);
                if (other.TryGetComponent(out IInteractable interactable))
                {
                    if(interactable.InteractionType == InteractionType.Manual)
                    {
                        interactable.Activate();
                    }
                }
            }
        }
    }
}
