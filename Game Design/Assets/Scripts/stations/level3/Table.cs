using items;
using items.handling;
namespace stations
{
    public class Table : ItemHolder
    {

        // IItemHolder

        public override bool CanReceiveItem(Item item) => true;

        public override Item GetItem()
        {
            return ReleaseLastItem();
        }

        public override Item PutItem(Item item)
        {
            return HoldItem(item);
        }

    }
}
