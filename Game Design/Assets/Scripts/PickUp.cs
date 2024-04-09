using managers;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;

    private GameObject _itemHolding;
    private PlayerMovement _playerMovement;

    private IMachineManager machineManager;
    private GameObject previouslyHighlightedMachine = null;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        HighlightNearestMachineWithinRadius();

        if (Input.GetKeyDown(KeyCode.F)) 
        {

            machineManager = HighlightNearestMachineWithinRadius();

            if (itemHolding)
            {
                //putting plastic in machine
                if (machineManager != null && Vector2.Distance(machineManager.MachineTransform.position, transform.position) <= machineManager.dropRadius /*&& itemHolding.CompareTag("Plastic")*/)
                {
                    machineManager.HoldItem(itemHolding);
                    itemHolding = null;
                }
                //dropping item on floor
                else
                {
                    DropItem();
                }
            }
            //taking item from machine
            else if (machineManager != null && Vector2.Distance(machineManager.MachineTransform.position, transform.position) <= machineManager.dropRadius && machineManager.IsHoldingItem())
            {
                TakeItemFromMachine();

            }
            //taking item from floor
            else
            {
                PickUpItem();
            }
        }
    }

    private void DropItem()
    {
        Rigidbody2D itemRigidbody = _itemHolding.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.simulated = true;
            itemRigidbody.velocity = Vector2.zero; // Stop any movement, or you could apply a throwing force here.
        }

        // Detach the item from the player and reset itemHolding
        _itemHolding.transform.SetParent(null);
        _itemHolding.transform.position = (Vector2)transform.position + _playerMovement.GetFacingDirection(); // Drop at the current player position plus direction.
        _itemHolding = null;
    }

    private void PickUpItem()
    {
        Vector2 direction = _playerMovement.GetFacingDirection();
        Collider2D pickUpItem = Physics2D.OverlapCircle((Vector2)transform.position + direction, .4f, pickUpMask);
        if (pickUpItem)
        {
            _itemHolding = pickUpItem.gameObject;
            _itemHolding.transform.position = holdSpot.position;
            _itemHolding.transform.parent = transform;
            if (_itemHolding.GetComponent<Rigidbody2D>())
                _itemHolding.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    private void TakeItemFromMachine()
    {
        itemHolding = machineManager.TakeItem();

        _itemHolding.transform.position = holdSpot.position;
        _itemHolding.transform.SetParent(holdSpot);

        Rigidbody2D itemRigidbody = _itemHolding.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.simulated = false;
        }
    }


    private IMachineManager HighlightNearestMachineWithinRadius()
    {
        GameObject nearestMachine = null;
        float nearestDistance = Mathf.Infinity;
        IMachineManager nearestMachineManager = null;

        foreach (GameObject machine in GameObject.FindGameObjectsWithTag("Machine"))
        {
            float distance = Vector2.Distance(machine.transform.position, transform.position);
            IMachineManager machineManager = machine.GetComponent<IMachineManager>();

            if (machineManager != null && distance <= machineManager.dropRadius && distance < nearestDistance)
            {
                nearestMachine = machine;
                nearestDistance = distance;
                nearestMachineManager = machineManager;
            }
        }

        if (nearestMachine != previouslyHighlightedMachine)
        {
            if (previouslyHighlightedMachine != null)
            {
                SetMachineColor(previouslyHighlightedMachine, Color.white);
            }

            if (nearestMachine != null)
            {
                SetMachineColor(nearestMachine, Color.grey);
                previouslyHighlightedMachine = nearestMachine;
            }
            else
            {
                previouslyHighlightedMachine = null;
            }
        }
        return nearestMachineManager;
    }

    private void SetMachineColor(GameObject machine, Color color)
    {
        SpriteRenderer renderer = machine.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }


}
