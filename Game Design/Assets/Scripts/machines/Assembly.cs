using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class Assembly : Machine
    {
        public Sprite trainSprite;
        public Timer timer;

        private bool _isHoldingParts;
        private bool _isHoldingWheels;
        private bool _isHoldingTrainItems
        {
            get
            {
                return _isHoldingParts && _isHoldingWheels;
            }
        }

        private Item remainingItem;
        public override void HoldItem(Item item)
        {
            if (!(item.CompareTag("TrainPartsPainted") || item.CompareTag("TrainWheels"))) return;

            if (item.CompareTag("TrainPartsPainted"))
            {
                _isHoldingParts = true;

                item.PickUp(holdSpot);
                item.IsHeldByMachine = true;

                if (!remainingItem) remainingItem = item;
                else if (remainingItem) itemHolding = item;
            }
            else if (item.CompareTag("TrainWheels"))
            {
                _isHoldingWheels = true;

                item.PickUp(holdSpot);
                item.IsHeldByMachine = true;

                if (!remainingItem) remainingItem = item;
                else if (remainingItem) itemHolding = item;
            }
            else
            {
                base.HoldItem(item);
            }


            if (_isHoldingTrainItems)
            {
                timer.StartTimer(5);
            }
        }

        private void Update()
        {
            if (timer.IsTimeUp() && _isHoldingTrainItems)
            {
                TransformItem(itemHolding);
                timer.ResetTimer();
            }
        }

        public override Item TakeItemFromMachine()
        {
            var item = base.TakeItemFromMachine();
            if (!timer.IsTimeUp())
            {
                timer.ResetTimer();
            }

            return item;
        }

        private void TransformItem(Item item)
        {
            item.SetSprite(trainSprite);
            item.tag = "Train";
            item.type = ItemType.Train;

            remainingItem.DeleteItem();

            _isHoldingParts = false;
            _isHoldingWheels = false;
        }
    }
}
