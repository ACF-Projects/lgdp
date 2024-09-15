using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{
    public class PointerCursorOnHover : MonoBehaviour
    {

        private bool _isEnabled = true;  // When false (through setter), will not set pointer on hover

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                // If we're disabling, and the mouse is hovering over this,
                // immediately call OnMouseExit()
                if (_isHoveringOver && !value)
                {
                    CursorManager.Instance.ResetCursor();
                }
                // If we're enabling, and the mouse is hovering over this,
                // immediately call OnMouseEnter()
                if (_isHoveringOver && value)
                {
                    CursorManager.Instance.SetPointerCursor();
                }
                _isEnabled = value;
            }
        }

        private bool _isHoveringOver = false;

        public void OnMouseEnter()
        {
            _isHoveringOver = true;
            if (!IsEnabled) { return; }
            CursorManager.Instance.SetPointerCursor();
        }

        public void OnMouseExit()
        {
            _isHoveringOver = false;
            if (!IsEnabled) { return; }
            CursorManager.Instance.ResetCursor();
        }

    }
}