using items;
using managers;

namespace machines
{
    public class Bin : Machine
    {
        public ItemManager itemManager;

        public override void HoldItem(Item item)
        {
            item.DeleteItem();
            itemManager.RefreshItems();
        }

    }
}
