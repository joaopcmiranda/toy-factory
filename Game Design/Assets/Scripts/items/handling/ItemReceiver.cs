using managers;
using UnityEngine;
namespace items.handling
{
    public abstract class ItemReceiver : MonoBehaviour, IItemHandler
    {
        protected ItemManager itemManager;

        public virtual void Start()
        {
            itemManager = FindObjectOfType<ItemManager>();
        }

        public Item GetItem()
        {
            return null;
        }

        protected abstract Item HandleItem(Item item);

        public abstract bool CanReceiveItem(Item item);

        public virtual Item PutItem(Item item)
        {
            if (CanReceiveItem(item))
            {
                return HandleItem(item);
            }
            else
            {
                return item;
            }
        }
    }
}
