using items;
using items.handling;

namespace stations
{
    public class MetalPress : ItemHolder
    {
        public int length = 5;

        public Timer timer;

        public override bool CanReceiveItem(Item item)
        {
            return item.type == ItemType.MetalSheet;
        }

        public override Item GetItem()
        {
            if (IsHoldingItem())
            {
                return ReleaseLastItem();
            }

            return null;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;
            item.DeleteItem();

            timer.StartTimer(length);
            return null;
        }

        private void Update()
        {
            if (timer.IsTimeUp() && timer.IsActive())
            {
                Transform();
                timer.ResetTimer();
            }
        }

        private void Transform()
        {
            var parts = itemManager.CreateItem(ItemType.MetalTrainBody, transform);
            HoldItem(parts);
        }
    }
}
