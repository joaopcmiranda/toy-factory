using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class Assembly : Machine
    {
        public TextMeshProUGUI uiText;
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

                if (!remainingItem) remainingItem = item;
                else if (remainingItem) itemHolding = item;
            }
            else if (item.CompareTag("TrainWheels"))
            {
                _isHoldingWheels = true;

                if (!remainingItem) remainingItem = item;
                else if (remainingItem) itemHolding = item;
            }
            else
            {
                base.HoldItem(item);
            }


            if (_isHoldingTrainItems)
            {
                uiText.text = "Assembling...";
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
            uiText.text = "Assembly done";
            item.SetSprite(trainSprite);
            item.tag = "Train";

            remainingItem.DeleteItem();

            _isHoldingParts = false;
            _isHoldingWheels = false;
        }
    }
}
