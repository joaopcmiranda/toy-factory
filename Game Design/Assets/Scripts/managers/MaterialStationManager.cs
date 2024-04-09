using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStationManager : MonoBehaviour, IMachineManager
{
    //assign following in inspector window

    public Transform holdSpot; //hold spot in machine
    public LayerMask pickUpMask; //layer machine picks items up from

    [SerializeField] private float _dropRadius = 1f;

    public float dropRadius => _dropRadius;
    public Transform MachineTransform => transform;

    private GameObject itemHolding;

    public GameObject rawMaterialType;

    private void Start()
    {
        GenerateNewMaterial();
    }

    //machine generates new item 
    public void HoldItem(GameObject item)
    {
        
    }

    //take the item out of the machine
    public GameObject TakeItem()
    {
        Debug.Log("-1");
        if (itemHolding != null)
        {
            GameObject item = itemHolding;
            Rigidbody2D itemRb = itemHolding.GetComponent<Rigidbody2D>();
            Debug.Log("0");
            if (itemRb != null)
            {
                itemRb.simulated = true; // Re-enable item physics
            }
            Debug.Log("1");
            item.transform.SetParent(null); // Detach the item from the machine


            GenerateNewMaterial(); // Generate a new material to replace the taken one
            Debug.Log("GenerateNewMaterial() called");

            itemHolding = null; // Clear the reference
            return item; // Return the taken item
        }
        return null;
    }

    private void GenerateNewMaterial()
    {
        if (rawMaterialType != null)
        {
            // Instantiate a new material at the holdSpot position
            GameObject newMaterial = Instantiate(rawMaterialType, holdSpot.position, Quaternion.identity, transform);
            // Optionally disable physics on the new material if not needed immediately
            Rigidbody2D itemRb = newMaterial.GetComponent<Rigidbody2D>();

            if (itemRb != null)
            {
                itemRb.simulated = true;
            }
            itemHolding = newMaterial;
            Debug.Log("generate new material complete");
        }
    }

    //checks if machine is holding an item
    public bool IsHoldingItem()
    {
        return itemHolding != null;
    }

    //make more methods as needed

    //does the machine need a timer method? Does it need a complete method that relies on input from the user?

}
