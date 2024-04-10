using items;
using machines;
using managers;
using UnityEngine;

namespace player
{
    public class PickUp : MonoBehaviour
    {
        public float pickUpRadius = .4f;

        public Transform holdSpot;
        public LayerMask pickUpMask;

        private Item _itemHolding;
        private PlayerMovement _playerMovement;
        private MachineManager _machineManager;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _machineManager = GameObject.FindWithTag("MachineManager").GetComponent<MachineManager>();
        }

        // Update is called once per frame
        void Update()
        {
            var nearestMachine = _machineManager.HighlightNearestMachineWithinRadius(transform);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_itemHolding)
                {
                    if (nearestMachine && IsNearMachine(nearestMachine))
                    {
                        nearestMachine.HoldItem(DropItem()); // put item in machine
                    }
                    else
                    {
                        DropItem(); // dropping item on floor
                    }
                }
                else
                {
                    if (nearestMachine && IsNearMachine(nearestMachine) && nearestMachine.IsHoldingItem())
                    {
                        TakeItemFromMachine(nearestMachine); // taking item from machine
                    }
                    else
                    {
                        PickUpItem(); // taking item from floor
                    }
                }
            }
        }

        private bool IsNearMachine(Machine machine)
        {
            return _machineManager && Vector2.Distance(machine.transform.position, transform.position) <= machine.dropRadius;
        }

        private Item DropItem()
        {
            _itemHolding.Drop();
            _itemHolding.transform.position = (Vector2)transform.position + _playerMovement.GetFacingDirection();
            var item = _itemHolding;
            _itemHolding = null;
            return item;
        }

        private void PickUpItem()
        {
            var direction = _playerMovement.GetFacingDirection();
            var pickUpItem = Physics2D.OverlapCircle((Vector2)transform.position + direction, pickUpRadius, pickUpMask);
            if (!pickUpItem)
            {
                return;
            }
            var item = pickUpItem.gameObject.GetComponent<Item>();
            if (item.IsHeldByMachine) // Check if item is held by machine
            {
                return; // Do not pick up item if it is held by a machine
            }
            item.PickUp(holdSpot);
            _itemHolding = item;
        }

        private void TakeItemFromMachine(Machine nearestMachine)
        {
            _itemHolding = nearestMachine.TakeItemFromMachine();
            _itemHolding.PickUp(holdSpot);
        }

    }
}
