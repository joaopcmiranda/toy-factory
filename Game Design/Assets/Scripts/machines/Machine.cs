using items;
using managers;
using UnityEngine;
namespace machines
{
    public abstract class Machine : MonoBehaviour
    {
        public Transform holdSpot;
        public float dropRadius = 1.5f;

        protected SpriteRenderer spriteRenderer;
        protected LevelManager levelManager;
        protected Item itemHolding;

        public virtual void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();
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

            item.IsHeldByMachine = false;

            item.Drop();

            itemHolding = null;

            return item;
        }

        public virtual void HoldItem(Item item)
        {
            if (itemHolding) return;

            item.PickUp(_heldby, holdSpot);
            item.IsHeldByMachine = true;

            itemHolding = item;
        }

        public virtual bool IsHoldingItem()
        {
            return itemHolding;
        }

    }
}
