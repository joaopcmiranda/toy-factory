using UnityEngine;
namespace UI
{
    public class Selectable : MonoBehaviour
    {
        public GameObject selectionIndicator;

        public bool isSelected = false;
        public virtual void Select()
        {
            isSelected = true;
            selectionIndicator.SetActive(true);
        }
        public virtual void Deselect()
        {
            isSelected = false;
            selectionIndicator.SetActive(false);
        }
    }
}
