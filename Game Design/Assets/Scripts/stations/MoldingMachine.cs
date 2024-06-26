using items;
using items.handling;

namespace stations
{
    public class MoldingMachine : ItemHolder
    {
        public Timer moldingTimer;
        public ItemType outputType = ItemType.Wheels;
        public ItemType inputType = ItemType.Metal;

        public override Item GetItem()
        {
            moldingTimer.ResetTimer();
            return ReleaseLastItem();
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;
            moldingTimer.StartTimer(5);
            return HoldItem(item);
        }

        private void Update()
        {
            if (moldingTimer.IsTimeUp() && moldingTimer.IsActive())
            {
                Transform();
                moldingTimer.ResetTimer();
            }
        }

        private void Transform()
        {
            ReleaseLastItem()
                ?.DeleteItem();
            var wheels = itemManager.CreateItem(outputType, transform);
            HoldItem(wheels);
        }

        public override bool CanReceiveItem(Item item)
        {
            return item.type == inputType;
        }
    }
}
