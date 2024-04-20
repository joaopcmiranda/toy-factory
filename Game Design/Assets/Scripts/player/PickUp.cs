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
        private PlayerMovement _playerMovement;
        private MachineManager _machineManager;
        private ItemManager _itemManager;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _machineManager = GameObject.FindWithTag("MachineManager").GetComponent<MachineManager>();
            _itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
            if (!mainCamera)
                mainCamera = Camera.main; // Ensure there is a main camera
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null)
                {
                    Item item = hit.collider.GetComponent<Item>();
                    Machine machine = hit.collider.GetComponent<Machine>();

                    if (_itemHolding)
                    {
                        if (machine && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius)
                        {
                            machine.HoldItem(DropItem());  // Put item in machine
                        }
                        else
                        {
                            DropItem();  // Dropping item on floor
                        }
                    }
                    else
                    {
                        if (machine && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius && machine.IsHoldingItem())
                        {
                            TakeItemFromMachine(machine);  // Taking item from machine
                        }
                        else if (item && !item.IsHeldByMachine)
                        {
                            PickUpItem(item);  // Picking up the item directly clicked
                        }
                    }
                }
            }
        }

        private Item DropItem()
        {
            _itemHolding.Drop();
            _itemHolding.transform.position = (Vector2)transform.position + _playerMovement.GetFacingDirection();
            var item = _itemHolding;
            _itemHolding = null;
            return item;
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
