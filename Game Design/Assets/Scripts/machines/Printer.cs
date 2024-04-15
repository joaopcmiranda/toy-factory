using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class Printer : Machine
    {

        public TextMeshProUGUI uiText;
        public Sprite trainSprite;
        public Timer timer;
 

        public override void HoldItem(Item item)
        {
            if (!item.CompareTag("Plastic")) return;

            base.HoldItem(item);
            uiText.text = "3D Printing...";
            timer.StartTimer(5);
        }

        private void Update()
        {
            if (timer.IsTimeUp())
            {
                if (itemHolding != null && itemHolding.CompareTag("Plastic"))
                {
                    TransformPlastic(itemHolding);
                    timer.ResetTimer();
                }
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

        private void TransformPlastic(Item item)
        {
            uiText.text = "3D Print Plastic done";
            item.SetSprite(trainSprite);
            item.tag = "TrainPartsUnpainted";
        }
    }
}
