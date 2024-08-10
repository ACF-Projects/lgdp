using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class RaycastTest : MonoBehaviour
    {
        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                Debug.Log("Hit");
            }
        }
    }
}
