using items;
using items.handling;

namespace stations
{
    public class PaintingStation : ItemHolder
    {
        public Timer timer;
        private bool _paintLoaded;


        public override Item GetItem()
        {
            if (!timer.IsTimeUp())
            {
                timer.ResetTimer();
            }
            return ReleaseLastItem();
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;
            Item returnItem;
            if (item.type == ItemType.Paint)
            {
                _paintLoaded = true;
                item.DeleteItem();
                returnItem = null;
            }
            else
            {
                returnItem = HoldItem(item);
            }

            if (_paintLoaded && IsHoldingItem())
            {
                timer.StartTimer(3);
            }

            return returnItem;
        }

        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.Paint:
                case ItemType.UnpaintedTrainParts:
                    return true;
                default:
                    return false;
            }
        }

        private void Update()
        {
            if (timer.IsTimeUp() && _paintLoaded && IsHoldingItem())
            {
                Transform();
                timer.ResetTimer();
            }
        }


        private void Transform()
        {
            ReleaseLastItem()
                .DeleteItem();

            var item = itemManager.CreateItem(ItemType.PaintedTrainParts, transform);
            HoldItem(item);
            _paintLoaded = false;
        }
    }
}
