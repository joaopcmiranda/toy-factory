using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrinterManager : MonoBehaviour
{

    public TextMeshProUGUI uiText;
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public float dropRadius = 1f;

    private GameObject itemHolding;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plastic")
        {
            uiText.text = "3D Print Plastic done";
        }
    }

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
}
