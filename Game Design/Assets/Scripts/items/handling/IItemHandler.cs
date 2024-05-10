using managers;
namespace items.handling
{
    public interface IItemHandler
    {
        public Item GetItem();
        public Item PutItem(Item item);
        public bool CanReceiveItem(Item item);
    }
}
