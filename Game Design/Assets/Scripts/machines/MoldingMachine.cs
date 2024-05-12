using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class MoldingMachine : Machine
    {

        public Sprite wheelsSprite;
        public Timer moldingTimer;


        public override void HoldItem(Item item)
        {
            if (!item.CompareTag("Metal")) return;

            base.HoldItem(item);
            moldingTimer.StartTimer(5);
        }

        private void Update()
        {
            if (moldingTimer.IsTimeUp())
            {
                if (itemHolding != null && itemHolding.CompareTag("Metal"))
                {
                    TransformMetal(itemHolding);
                    moldingTimer.ResetTimer();
                }
            }
        }

        public override Item TakeItemFromMachine()
        {
            var item = base.TakeItemFromMachine();
            if (!moldingTimer.IsTimeUp())
            {
                moldingTimer.ResetTimer();
            }
            return item;
        }

        private void TransformMetal(Item item)
        {
            item.SetSprite(wheelsSprite);
            item.tag = "TrainWheels";
            item.type = ItemType.Wheels;
        }
    }
}