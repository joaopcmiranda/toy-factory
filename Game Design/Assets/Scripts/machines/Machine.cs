using items;
using UnityEngine;
namespace machines
{
    public abstract class Machine : MonoBehaviour
    {
        public Transform holdSpot;
        public float dropRadius = 1f;

        protected SpriteRenderer spriteRenderer;
        protected Item itemHolding;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetMachineColor(Color color)
        {
            if (spriteRenderer)
            {
                spriteRenderer.color = color;
            }
        }

        public virtual Item TakeItemFromMachine()
        {
            if (!itemHolding) return null;
            // Retrieve the item from the machine
            var item = itemHolding;

            item.Drop();

            itemHolding = null;

            return item;
        }

        public virtual void HoldItem(Item item)
        {
            if (itemHolding) return;

            item.PickUp(holdSpot);
            itemHolding = item;
        }

        public bool IsHoldingItem()
        {
            return itemHolding;
        }

    }
}
