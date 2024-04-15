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
        private bool _paintLoaded;


        public override void HoldItem(Item item)
        {
            if (!(item.CompareTag("TrainPartsUnpainted") || item.CompareTag("Paint"))) return;

            if (item.CompareTag("Paint"))
            {
                _paintLoaded = true;
                item.DeleteItem();
            }
            else
            {
                base.HoldItem(item);
            }

            if (_paintLoaded && itemHolding && itemHolding.CompareTag("TrainPartsUnpainted"))
            {
                uiText.text = "Painting...";
                timer.StartTimer(3);
            }

        }

        private void Update()
        {
            if (timer.IsTimeUp() && _paintLoaded && itemHolding && itemHolding.CompareTag("TrainPartsUnpainted"))
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
            item.tag = "TrainPartsPainted";
            _paintLoaded = false;
        }
    }
}
