using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class Printer : Machine
    {

        public TextMeshProUGUI uiText;

        public Sprite trainSprite;

        public override void HoldItem(Item item)
        {
            base.HoldItem(item);
            uiText.text = "3D Printing...";
        }

        public override Item TakeItemFromMachine()
        {
            var item = base.TakeItemFromMachine();

            TransformPlastic(item);
            return item;
        }

        private void TransformPlastic(Item item)
        {
            if (!item.CompareTag("Plastic")) return;

            uiText.text = "3D Print Plastic done";
            item.SetSprite(trainSprite);
            item.tag = "Train";
        }
    }
}
