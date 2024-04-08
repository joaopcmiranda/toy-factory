using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;

    private GameObject itemHolding;
    private PlayerMovement playerMovement;

    private PrinterManager printerManager;

    private void Awake()
    {
        printerManager = FindObjectOfType<PrinterManager>();
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (itemHolding) 
            {
                if (Vector2.Distance(printerManager.transform.position, transform.position) <= printerManager.dropRadius)
                {
                    printerManager.HoldItem(itemHolding); // The printer takes the item
                    itemHolding = null; // Clear the reference as the printer now holds the item
                }
                else
                {
                    DropItem(); // Normal drop procedure
                }
            }
            else 
            {
                PickUpItem();
            }
        }
    }

    private void DropItem()
    {
        Rigidbody2D itemRigidbody = itemHolding.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.simulated = true;
            itemRigidbody.velocity = Vector2.zero; // Stop any movement, or you could apply a throwing force here.
        }

        // Detach the item from the player and reset itemHolding
        itemHolding.transform.SetParent(null);
        itemHolding.transform.position = (Vector2)transform.position + playerMovement.GetFacingDirection(); // Drop at the current player position plus direction.
        itemHolding = null;
    }

    private void PickUpItem()
    {
        Vector2 direction = playerMovement.GetFacingDirection();
        Collider2D pickUpItem = Physics2D.OverlapCircle((Vector2)transform.position + direction, .4f, pickUpMask);
        if (pickUpItem)
        {
            itemHolding = pickUpItem.gameObject;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;
            if (itemHolding.GetComponent<Rigidbody2D>())
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
