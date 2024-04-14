using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class PaintingStation : Machine
    {

        public TextMeshProUGUI uiText;
        public Sprite trainSprite;
        public Timer timer;


        public override void HoldItem(Item item)
        {
            if (!(item.CompareTag("TrainParts") || item.CompareTag("Paint"))) return;

            base.HoldItem(item);
            uiText.text = "Painting...";
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
            uiText.text = "Painting done";
            item.SetSprite(trainSprite);
            item.tag = "Train";
        }
    }
}
