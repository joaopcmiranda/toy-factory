using UnityEngine;
namespace UI
{

    public enum CursorType
    {
        Default,
        Select,
        DropOff,
        PickUp,
        None
    }

    public class CursorManager : MonoBehaviour
    {
        public Texture2D defaultCursorTexture;
        public Texture2D selectCursorTexture;
        public Texture2D dropOffCursorTexture;
        public Texture2D pickUpCursorTexture;

        public Vector2 hotSpot = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        // Start is called before the first frame update
        void Start()
        {

            Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public void SetCursor(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.Default:
                    Cursor.visible = true;
                    Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
                    break;
                case CursorType.Select:
                    Cursor.visible = true;
                    Cursor.SetCursor(selectCursorTexture, hotSpot, cursorMode);
                    break;
                case CursorType.DropOff:
                    Cursor.visible = true;
                    Cursor.SetCursor(dropOffCursorTexture, hotSpot, cursorMode);
                    break;
                case CursorType.PickUp:
                    Cursor.visible = true;
                    Cursor.SetCursor(pickUpCursorTexture, hotSpot, cursorMode);
                    break;
                case CursorType.None:
                    Cursor.visible = false;
                    break;
                default:
                    Cursor.visible = true;
                    Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
                    break;
            }
        }
    }
}
