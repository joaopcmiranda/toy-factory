using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemplateMachineManager : MonoBehaviour
{
    //assign following in inspector window

    public TextMeshProUGUI uiText; //UI element that needs changing

    public Transform holdSpot; //hold spot in machine
    public LayerMask pickUpMask; //layer machine picks items up from

    public float dropRadius = 1f;

    private GameObject itemHolding;
    public Sprite itemTransformation; //sprite that you will transform held into into


    //machine holds item
    public void HoldItem(GameObject item)
    {
        if (itemHolding == null) // If not already holding an item
        {
            itemHolding = item;
            itemHolding.transform.position = holdSpot.position; // Move to hold spot
            itemHolding.transform.SetParent(transform); // Parent to the printer

            // Disable physics because we don't want the item to fall or be affected by other forces
            Rigidbody2D itemRb = itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.simulated = false;
            }

            //edit what happens when the item is held. e.g.

            uiText.text = ""; // Update UI

            //might want to make a method that initiates a timer or an animation

            /*placeholder:*/
            TransformItem(itemHolding);
            
        }
    }

    //take the item out of the machine
    public GameObject TakeItem()
    {
        if (itemHolding != null)
        {
            // Retrieve the item from the machine
            GameObject item = itemHolding;

            // re-enable item physics
            Rigidbody2D itemRb = itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.simulated = true;
            }

            item.transform.SetParent(null);

            itemHolding = null;

            return item;
        }
        return null;
    }

    //change the item that is held by the machine at some point. This might need to be called by a script on Player object.
    private void TransformItem(GameObject itemHolding)
    {
        if (itemHolding.CompareTag("SomeTag"))
        {
            //change UI
            uiText.text = "something";
            SpriteRenderer plasticSpriteRenderer = itemHolding.GetComponent<SpriteRenderer>();
            plasticSpriteRenderer.sprite = itemTransformation;

            //set a different tag for the item, because it has changed into something else
            itemHolding.tag = "SomeTag";
        }
    }

    //checks if machine is holding an item
    public bool IsHoldingItem()
    {
        if (itemHolding != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //make more methods as needed

    //does the machine need a timer method? Does it need a complete method that relies on input from the user?

}
