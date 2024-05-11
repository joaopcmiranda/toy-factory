using System.Collections.Generic;
using managers;
using UI;
using UnityEngine;

namespace items.handling
{
    public abstract class ItemHolder : MonoBehaviour, IItemHandler
    {
        public List<Transform> holdSpots;

        protected readonly List<Item> itemsHeld = new List<Item>();
        protected ItemManager itemManager;

        public abstract Item GetItem();
        public abstract Item PutItem(Item item);
        public abstract bool CanReceiveItem(Item item);

        public virtual void Start()
        {
            itemManager = FindObjectOfType<ItemManager>();
        }

        protected void DestroyItem(Item item)
        {
            itemsHeld.Remove(item);
            item.DeleteItem();
        }

        protected void DestroyAllItems()
        {
            foreach (var item in itemsHeld)
            {
                item.DeleteItem();
            }
            itemsHeld.Clear();
        }

        protected Item ReleaseItemAtIndex(int index)
        {
            if (itemsHeld.Count < index - 1) return null;
            // Retrieve the item from the machine
            var item = itemsHeld[index];

            item.Drop();

            itemsHeld.RemoveAt(index);

            return item;
        }

        protected Item ReleaseLastItem()
        {

            if (itemsHeld.Count == 0) return null;
            // Retrieve the item from the machine
            var item = itemsHeld[^1];

            item.Drop();

            itemsHeld.RemoveAt(itemsHeld.Count - 1);

            return item;
        }

        protected Item HoldItem(Item item)
        {
            if (itemsHeld.Count < holdSpots.Count)
            {
                item.PickUp(this, holdSpots[itemsHeld.Count]);
                itemsHeld.Add(item);
                return null;
            }
            else
            {
                // replace the item in the last hold spot
                var itemToReplace = itemsHeld[^1];

                itemToReplace.Drop();

                itemsHeld[^1] = item;
                item.PickUp(this, holdSpots[itemsHeld.Count - 1]);
                return itemToReplace;
            }
        }

        protected virtual bool IsHoldingItem()
        {
            return itemsHeld.Count > 0;
        }

        protected virtual int GetHeldItemCount()
        {
            return itemsHeld.Count;
        }

    }
}
