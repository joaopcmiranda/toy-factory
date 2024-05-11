using managers;
using UI;
using UnityEngine;

namespace items.handling
{
    public abstract class ItemProvider : MonoBehaviour, IItemHandler
    {
        public Transform provideSpot;
        private Item _item;

        protected ItemManager itemManager;

        public abstract ItemType itemType
        {
            get;
        }

        public virtual void Start()
        {
            itemManager = FindObjectOfType<ItemManager>();
            var item = itemManager.CreateItem(itemType, transform);
            if (provideSpot)
            {
                item.PickUp(this, provideSpot);
            }
            _item = item;
        }

        public virtual Item GetItem()
        {

            if (provideSpot)
            {
                var item = _item;
                item.Drop();
                _item = itemManager.CreateItem(itemType, provideSpot);
                _item.PickUp(this, provideSpot);
                return item;
            }
            else
            {
                return itemManager.CreateItem(itemType, transform);
            }
        }

        public virtual Item PutItem(Item item)
        {
            return item;
        }

        public virtual bool CanReceiveItem(Item item)
        {
            return false;
        }
    }
}
