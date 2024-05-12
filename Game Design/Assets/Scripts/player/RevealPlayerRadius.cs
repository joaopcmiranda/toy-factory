using TMPro;
using UnityEngine;

namespace machines
{
    public class RevealPlayerRadius : MonoBehaviour
    {
        public GameObject radius;
        private SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = radius.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            
        }

        public void OnMouseOver()
        {
            spriteRenderer.enabled = true;
        }

        public void OnMouseExit()
        {
            spriteRenderer.enabled = false;
        }

    }
}
