using items;
using managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace machines
{
    public class PaintingStation : Machine
    {

        public Sprite paintedTrainPartsSprite;
        public Timer timer;
        private bool _paintLoaded;
        private ItemManager _itemManager;


        public override void Start()
        {
            base.Start();
            _itemManager = FindObjectOfType<ItemManager>();
        }

        public override void HoldItem(Item item)
        {
            if (!(item.CompareTag("TrainPartsUnpainted") || item.CompareTag("Paint"))) return;

            if (item.CompareTag("Paint"))
            {
                _paintLoaded = true;
                item.DeleteItem();
                _itemManager.RefreshItems();
            }
            else
            {
                base.HoldItem(item);
            }

            if (_paintLoaded && itemHolding && itemHolding.CompareTag("TrainPartsUnpainted"))
            {
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
            item.SetSprite(paintedTrainPartsSprite);
            item.tag = "TrainPartsPainted";
            _paintLoaded = false;
        }
    }
}
