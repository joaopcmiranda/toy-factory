using UnityEngine;
namespace UI
{
    public class Selectable : MonoBehaviour
    {
        public GameObject selectionIndicator;

        public void Select()
        {
            selectionIndicator.SetActive(true);
        }
        public void Deselect()
        {
            selectionIndicator.SetActive(false);
        }
    }
}
