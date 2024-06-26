using items;
using machines;
using managers;
using UnityEngine;
namespace player
{
    public class PickUp_Level4 : MonoBehaviour
    {
        public Transform holdSpot;
        public Camera mainCamera; // Assign the main camera in the Inspector       

        public Item_Level4 _itemHolding;
        public Character_Level4 _character;
        public MachineManager_Level4 _machineManager;
        public ItemManager_Level4 _itemManager;
        public AudioManager audioManager;

        private float pickUpRadius = 1.3f;

        private void Start()
        {
            _character = GetComponent<Character_Level4>();
            _machineManager = GameObject.FindWithTag("MachineManager").GetComponent<MachineManager_Level4>();
            _itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager_Level4>();
            audioManager = FindObjectOfType<AudioManager>();
            if (!mainCamera)
                mainCamera = Camera.main; // Ensure there is a main camera
        }

        void Update()
        {
            _machineManager.HighlightMousedOverMachineWithinRadius(transform);

            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                Debug.Log("Clicking!");
                Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity);

                if (_itemHolding)
                {
                    // Attempt to drop the item either on the ground or into a machine if within range
                    if (hit.collider != null)
                    {
                        Machine_Base_Level4 machine = hit.collider.GetComponent<Machine_Base_Level4>();
                        if (machine && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius)
                        {
                            machine.HoldItem(DropItem(mouseWorldPos)); // Drop item into machine
                            audioManager.PlayMachine();
                        }
                        else if ((mouseWorldPos - (Vector2)transform.position).sqrMagnitude <= Mathf.Pow(pickUpRadius, 2))
                        {
                            DropItem(mouseWorldPos); // Drop the item at the clicked position on the ground
                            audioManager.PlayItem();
                        }
                    }
                    else if ((mouseWorldPos - (Vector2)transform.position).sqrMagnitude <= Mathf.Pow(pickUpRadius, 2))
                    {
                        DropItem(mouseWorldPos); // Drop the item at the clicked position on the ground
                        audioManager.PlayItem();
                    }
                }
                else
                {
                    // No item is currently being held, check for picking up items or interacting with machines directly
                    if (hit.collider != null)
                    {
                        Item_Level4 item = hit.collider.GetComponent<Item_Level4>();
                        Machine_Base_Level4 machine = hit.collider.GetComponent<Machine_Base_Level4>();

                        if (machine && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius && machine.IsHoldingItem())
                        {
                            TakeItemFromMachine(machine);  // Taking item from machine
                            audioManager.PlayItem();
                        }
                        else if (item && !item.IsHeldByMachine && Vector2.Distance(item.transform.position, transform.position) <= pickUpRadius)
                        {
                            PickUpItem(item);  // Picking up the item directly clicked
                            audioManager.PlayItem();
                        }
                    }
                }
            }
        }

        private Item_Level4 DropItem(Vector2 dropPosition)
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

        private void PickUpItem(Item_Level4 item)
        {
            if (item == null) return;
            item.PickUp(holdSpot);
            _itemHolding = item;
        }

        private void TakeItemFromMachine(Machine_Base_Level4 machine)
        {
            _itemHolding = machine.TakeItemFromMachine();
            _itemHolding.PickUp(holdSpot);
        }
    }
}
