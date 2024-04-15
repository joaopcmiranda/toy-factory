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

        private bool holdTrainPartsPainted = false;
        private bool holdTrainWheels = false;
        private bool holdTrainItems => holdTrainPartsPainted && holdTrainWheels;

        private Item remainingItem = null; //I wanted to show two items, and to pick it up
        public override void HoldItem(Item item)
        {
            if (!(item.CompareTag("TrainPartsPainted") || item.CompareTag("TrainWheels"))) return;

            if (item.CompareTag("TrainPartsPainted"))
            {
                holdTrainPartsPainted = true;

                base.HoldItem(item);
                if (remainingItem == null)
                {
                    remainingItem = item;
                }
                else if (remainingItem != null)
                {
                    itemHolding = item;
                }
            }
            else
            {
                base.HoldItem(item);
            }

            if (item.CompareTag("TrainWheels"))
            {
                holdTrainWheels = true;

                base.HoldItem(item);
                if (remainingItem == null)
                {
                    remainingItem = item;
                }
                else if (remainingItem != null)
                {
                    itemHolding = item;
                }
            }
            else
            {
                base.HoldItem(item);
            }

            if (holdTrainItems)
            {
                uiText.text = "Assembling...";
                timer.StartTimer(5);
                //timer.StartTimer(0); //instant effect, literally no timer
            }
            Debug.Log("item: " + item);
            Debug.Log("itemHolding: " + itemHolding);
        }

        private void Update()
        {
            if (timer.IsTimeUp() && holdTrainItems)
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

            holdTrainPartsPainted = false;
            holdTrainWheels = false;
        }
    }
}
