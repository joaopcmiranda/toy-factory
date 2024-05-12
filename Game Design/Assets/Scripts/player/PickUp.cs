using items;
using items.handling;
using UnityEngine;

namespace player
{
    public class PickUp : ItemHolder
    {

        private AudioManager _audioManager;
        private Character _character;
        private SelectionRayCaster _selectionRayCaster;
        private KeyCode _actionKey = KeyCode.Q;

        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _selectionRayCaster = GetComponent<SelectionRayCaster>();
            _character = GetComponent<Character>();
        }

        public override void Start()
        {
            base.Start();

            _actionKey = _character.controls == CharacterControls.Keyboard1 ? KeyCode.Q : KeyCode.O;
        }

        void Update()
        {
            if (Input.GetKeyDown(_actionKey))
            {
                if (_selectionRayCaster.IsObjectSelected())
                {
                    var target = _selectionRayCaster.GetSelectedObject();
                    var itemHandler = target.GetComponent<IItemHandler>();
                    if (itemHandler != null)
                    {
                        if (IsHoldingItem())
                        {
                            itemHandler.PutItem(GetItem());
                        }
                        else
                        {
                            PutItem(itemHandler.GetItem());
                        }
                        _audioManager.PlayMachine();
                    }
                    else if (target.TryGetComponent<Item>(out var item))
                    {
                        if (IsHoldingItem())
                        {
                            DropItem(_character.GetFacingDirection() * 0.75f);
                        }
                        PutItem(item);
                        _audioManager.PlayItem();
                    }
                }
                else
                {
                    DropItem(_character.GetFacingDirection() * 0.75f);
                    _audioManager.PlayItem();
                }
            }
        }

        private Item DropItem(Vector2 dropPosition)
        {
            if (IsHoldingItem())
            {
                var item = ReleaseLastItem();
                item.transform.position = transform.position + (Vector3)dropPosition;
                return item;
            }
            return null;
        }

        public override Item GetItem()
        {
            return ReleaseLastItem();
        }

        public override Item PutItem(Item item)
        {
            return HoldItem(item);
        }

        public override bool CanReceiveItem(Item item) => true;

    }
}
