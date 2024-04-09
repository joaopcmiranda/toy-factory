using machines;
using UnityEngine;

namespace player
{
    public class PickUp : MonoBehaviour
    {
        public Transform holdSpot;
        public LayerMask pickUpMask;

        private GameObject _itemHolding;
        private PlayerMovement _playerMovement;

        private IMachine machineManager;
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

                if (_itemHolding)
                {
                    //putting plastic in machine
                    if (machineManager != null && Vector2.Distance(machineManager.MachineTransform.position, transform.position) <= machineManager.DropRadius /*&& itemHolding.CompareTag("Plastic")*/)
                    {
                        machineManager.HoldItem(_itemHolding);
                        _itemHolding = null;
                    }
                    //dropping item on floor
                    else
                    {
                        DropItem();
                    }
                }
                //taking item from machine
                else if (machineManager != null && Vector2.Distance(machineManager.MachineTransform.position, transform.position) <= machineManager.DropRadius && machineManager.IsHoldingItem())
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
            _itemHolding = machineManager.TakeItem();

            _itemHolding.transform.position = holdSpot.position;
            _itemHolding.transform.SetParent(holdSpot);

            Rigidbody2D itemRigidbody = _itemHolding.GetComponent<Rigidbody2D>();
            if (itemRigidbody != null)
            {
                itemRigidbody.simulated = false;
            }
        }


        private IMachine HighlightNearestMachineWithinRadius()
        {
            GameObject nearestMachine = null;
            var nearestDistance = Mathf.Infinity;
            IMachine nearestMachineManager = null;

            foreach (var machine in GameObject.FindGameObjectsWithTag("Machine"))
            {
                var distance = Vector2.Distance(machine.transform.position, transform.position);
                var machineManager = machine.GetComponent<IMachine>();

                if (machineManager != null && distance <= machineManager.DropRadius && distance < nearestDistance)
                {
                    nearestMachine = machine;
                    nearestDistance = distance;
                    nearestMachineManager = machineManager;
                }
            }

            if (nearestMachine != previouslyHighlightedMachine)
            {
                if (previouslyHighlightedMachine)
                {
                    SetMachineColor(previouslyHighlightedMachine, Color.white);
                }

                if (nearestMachine)
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
            var renderer = machine.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = color;
            }
        }


    }
}
