using TMPro;
using UnityEngine;

namespace managers.machines
{
    public class TemplateMachineManager : MonoBehaviour
    {
        //assign following in inspector window

        public TextMeshProUGUI uiText; //UI element that needs changing

        public Transform holdSpot; //hold spot in machine
        public LayerMask pickUpMask; //layer machine picks items up from

        [SerializeField] private float _dropRadius = 1f;

        public float dropRadius => _dropRadius;
        public Transform MachineTransform => transform;

        private GameObject _itemHolding;
        public Sprite itemTransformation; //sprite that you will transform held into into


        //machine holds item
        public void HoldItem(GameObject item)
        {
            if (_itemHolding) return; // If not already holding an item

            _itemHolding = item;
            _itemHolding.transform.position = holdSpot.position; // Move to hold spot
            _itemHolding.transform.SetParent(transform); // Parent to the printer

            // Disable physics because we don't want the item to fall or be affected by other forces
            var itemRb = _itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.simulated = false;
            }

            //edit what happens when the item is held. e.g.

            uiText.text = ""; // Update UI

            //might want to make a method that initiates a timer or an animation

            /*placeholder:*/
            TransformItem(_itemHolding);
        }

        //take the item out of the machine
        public GameObject TakeItem()
        {
            if (!_itemHolding) return null;

            // Retrieve the item from the machine
            var item = _itemHolding;

            // re-enable item physics
            var itemRb = _itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.simulated = true;
            }

            item.transform.SetParent(null);

            _itemHolding = null;

            return item;
        }

        //change the item that is held by the machine at some point. This might need to be called by a script on Player object.
        private void TransformItem(GameObject itemHolding)
        {
            if (!itemHolding.CompareTag("SomeTag")) return;

            //change UI
            uiText.text = "something";
            var plasticSpriteRenderer = itemHolding.GetComponent<SpriteRenderer>();
            plasticSpriteRenderer.sprite = itemTransformation;

            //set a different tag for the item, because it has changed into something else
            itemHolding.tag = "SomeTag";
        }

        //checks if machine is holding an item
        public bool IsHoldingItem()
        {
            return _itemHolding;
        }

        //make more methods as needed

        //does the machine need a timer method? Does it need a complete method that relies on input from the user?

    }
}
