using items;
using items.handling;

namespace stations
{
    public class Bin : ItemReceiver
    {
        protected override Item HandleItem(Item item)
        {
            item.DeleteItem();
            return null;
        }

        public override bool CanReceiveItem(Item item)
        {
            return true;
        }
    }
}
