using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class PaintingStation : Machine
    {

        public TextMeshProUGUI uiText;
        public Sprite paintedTrainPartsSprite;
        public Timer timer;


        public override void HoldItem(Item item)
        {
            if (!(item.CompareTag("TrainParts") || item.CompareTag("Paint"))) return;

            base.HoldItem(item);
            uiText.text = "Painting...";
            timer.StartTimer(3);
        }

        private void Update()
        {
            if (timer.IsTimeUp() && itemHolding != null && itemHolding.CompareTag("TrainParts"))
            {
                PaintTrainParts(itemHolding);
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

        private void PaintTrainParts(Item item)
        {
            uiText.text = "Painting done";
            item.SetSprite(paintedTrainPartsSprite);
            item.tag = "Train";
        }
    }
}
