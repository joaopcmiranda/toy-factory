using items;
using UnityEngine;
using TMPro;
namespace machines
{
    public abstract class Machine : MonoBehaviour
    {
        public Transform holdSpot;
        public float dropRadius = 1f;

        public TextMeshProUGUI stationNameText;
        public string stationName = "";

        protected SpriteRenderer spriteRenderer;
        protected Item itemHolding;

        public virtual void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            stationNameText.SetText(stationName);
            ShowMachineName(false);
        }

        public void ShowMachineName(bool show)
        {
            stationNameText.enabled = show;
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

            item.PickUp(holdSpot);
            item.IsHeldByMachine = true;

            itemHolding = item;
        }

        public virtual bool IsHoldingItem()
        {
            return itemHolding;
        }

    }
}
