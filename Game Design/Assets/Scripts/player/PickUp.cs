using items;
using machines;
using managers;
using UnityEngine;

namespace player
{
    public class PickUp : MonoBehaviour
    {
        public Transform holdSpot;
        public Camera mainCamera; // Assign the main camera in the Inspector

        private Item _itemHolding;
        private Character _character;
        private MachineManager _machineManager;
        private ItemManager _itemManager;

        private float pickUpRadius = 1.3f;

        private void Start()
        {
            _character = GetComponent<Character>();
            _machineManager = GameObject.FindWithTag("MachineManager").GetComponent<MachineManager>();
            _itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
            if (!mainCamera)
                mainCamera = Camera.main; // Ensure there is a main camera
        }

        void Update()
        {
            _machineManager.HighlightMousedOverMachineWithinRadius(transform);

            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity);

                if (_itemHolding)
                {
                    // Attempt to drop the item either on the ground or into a machine if within range
                    if (hit.collider != null)
                    {
                        Machine machine = hit.collider.GetComponent<Machine>();
                        if (machine && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius)
                        {
                            machine.HoldItem(DropItem(mouseWorldPos)); // Drop item into machine
                        }
                        else if ((mouseWorldPos - (Vector2)transform.position).sqrMagnitude <= Mathf.Pow(pickUpRadius, 2))
                        {
                            DropItem(mouseWorldPos); // Drop the item at the clicked position on the ground
                        }
                    }
                    else if ((mouseWorldPos - (Vector2)transform.position).sqrMagnitude <= Mathf.Pow(pickUpRadius, 2))
                    {
                        DropItem(mouseWorldPos); // Drop the item at the clicked position on the ground
                    }
                }
                else
                {
                    // No item is currently being held, check for picking up items or interacting with machines directly
                    if (hit.collider != null)
                    {
                        Item item = hit.collider.GetComponent<Item>();
                        Machine machine = hit.collider.GetComponent<Machine>();

                        if (machine && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius && machine.IsHoldingItem())
                        {
                            TakeItemFromMachine(machine);  // Taking item from machine
                        }
                        else if (item && !item.IsHeldByMachine && Vector2.Distance(item.transform.position, transform.position) <= pickUpRadius)
                        {
                            PickUpItem(item);  // Picking up the item directly clicked
                        }
                    }
                }
            }
        }

        private Item DropItem(Vector2 dropPosition)
        {
            if (_itemHolding)
            {
                _itemHolding.transform.position = dropPosition;
                _itemHolding.Drop();
                var item = _itemHolding;
                _itemHolding = null;
                return item;
            }
            return null;
        }

        private void PickUpItem(Item item)
        {
            if (item == null) return;
            item.PickUp(holdSpot);
            _itemHolding = item;
        }

        private void TakeItemFromMachine(Machine machine)
        {
            _itemHolding = machine.TakeItemFromMachine();
            _itemHolding.PickUp(holdSpot);
        }
    }
}
