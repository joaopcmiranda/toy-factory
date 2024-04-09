using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrinterManager : MonoBehaviour, IMachineManager
{

    public TextMeshProUGUI uiText;
    public Transform holdSpot;
    public LayerMask pickUpMask;
    [SerializeField] private float _dropRadius = 1f;

    public float dropRadius => _dropRadius;
    public Transform MachineTransform => transform;

    private GameObject itemHolding;
    public Sprite trainSprite;

    public void HoldItem(GameObject item)
    {
        if (itemHolding == null) // If not already holding an item
        {
            itemHolding = item;
            itemHolding.transform.position = holdSpot.position; // Move to printer's hold spot
            itemHolding.transform.SetParent(transform); // Parent to the printer

            // Optionally disable physics if you don't want the item to fall or be affected by other forces
            Rigidbody2D itemRb = itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.simulated = false;
            }

            uiText.text = "3D Printing..."; // Update UI to show printing status
        }
    }

    public GameObject TakeItem()
    {
        if (itemHolding != null)
        {
            // Retrieve the item from the printer
            GameObject item = itemHolding;

            // Optionally, if you disabled physics when putting the item in the printer, re-enable it here
            Rigidbody2D itemRb = itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.simulated = true;
            }

            item.transform.SetParent(null);

            TransformPlastic(item);

            itemHolding = null;

            return item;
        }
        return null;
    }

    private void TransformPlastic(GameObject itemHolding)
    {
        if (itemHolding.CompareTag("Plastic"))
        {
            uiText.text = "3D Print Plastic done";
            SpriteRenderer plasticSpriteRenderer = itemHolding.GetComponent<SpriteRenderer>();
            plasticSpriteRenderer.sprite = trainSprite;
            itemHolding.tag = "Train";
        }
    }

    public bool IsHoldingItem()
    {
        if (itemHolding != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

}
