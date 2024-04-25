using System;
using UnityEngine;
namespace UI
{
    public class MouseOverCursor : MonoBehaviour
    {
        public CursorType cursorType = CursorType.Default;
        private CursorManager _cursorManager;

        private void Start()
        {
            _cursorManager = FindObjectOfType<CursorManager>();
        }

        private void OnMouseEnter()
        {
           _cursorManager.SetCursor(cursorType);
        }

        private void OnMouseExit()
        {
            _cursorManager.SetCursor(CursorType.Default);
        }
    }
}
