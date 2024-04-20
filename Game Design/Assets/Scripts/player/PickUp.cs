using items;
using machines;
using managers;
using UnityEngine;

namespace player
{
    public class PickUp : MonoBehaviour
    {
        public float pickUpRadius = 1f;
        public Transform holdSpot;
        public LayerMask pickUpMask;

        private Item _itemHolding;
        private PlayerMovement _playerMovement;
        private MachineManager _machineManager;
        private ItemManager _itemManager;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _machineManager = GameObject.FindWithTag("MachineManager").GetComponent<MachineManager>();
            _itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // or use any other button or key
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, pickUpMask);
                foreach (Collider2D hit in hits)
                {
                    Item item = hit.GetComponent<Item>();
                    Machine machine = hit.GetComponent<Machine>();

                    if (_itemHolding)
                    {
                        if (machine && IsNearMachine(machine))
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
                        if (machine && IsNearMachine(machine) && machine.IsHoldingItem())
                        {
                            TakeItemFromMachine(machine);  // Taking item from machine
                        }
                        else if (item && !item.IsHeldByMachine)
                        {
                            PickUpItem(item);  // Picking up the item from the floor
                        }
                    }
                }
            }
        }

        private bool IsNearMachine(Machine machine)
        {
            return Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius;
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
