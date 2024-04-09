using items;
using TMPro;
using UnityEngine;

namespace machines
{
    public class TemplateMachineManager : Machine
    {
        //assign following in inspector window

        public TextMeshProUGUI uiText; //UI element that needs changing
        public Sprite transformedSprite; //sprite that the item will change into

        //machine holds item
        public new void HoldItem(Item item)
        {
            base.HoldItem(item);
            uiText.text = ""; // Update UI

            //might want to make a method that initiates a timer or an animation

            /*placeholder:*/
            TransformItem(itemHolding);
        }


        //change the item that is held by the machine at some point. This might need to be called by a script on Player object.
        private void TransformItem(Item item)
        {
            if (!item.CompareTag("SomeTag")) return;

            //change UI
            uiText.text = "something";
            var plasticSpriteRenderer = item.GetComponent<SpriteRenderer>();
            plasticSpriteRenderer.sprite = transformedSprite;

            //set a different tag for the item, because it has changed into something else
            item.tag = "SomeTag";
        }

        //make more methods as needed

        //does the machine need a timer method? Does it need a complete method that relies on input from the user?

    }
}
