using items;
using items.handling;
using UnityEngine;

namespace stations
{
    public class PlasticInjector : ItemHolder
    {
        public int length = 5;
        public Timer timer;
        public GameObject redChoiceMenu;
        public GameObject greenChoiceMenu;
        public GameObject blueChoiceMenu;

        private ItemType _inputType;
        private ItemType _selectedProduction;
        private bool _isSelectingProduction;

        private Animator _animator;
        private static readonly int RunMachineAnimationTrigger = Animator.StringToHash("Run Machine");
        private static readonly int ColorNrgb = Animator.StringToHash("ColorNRGB");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.RedPlastic:
                case ItemType.BluePlastic:
                case ItemType.GreenPlastic:
                    return true;
                default:
                    return false;
            }
        }

        public override Item GetItem()
        {
            if (IsHoldingItem() && !_isSelectingProduction)
            {
                return ReleaseLastItem();
            }

            return null;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item) || timer.IsActive()) return item;

            _inputType = item.type;

            SetAnimationColor(_inputType);

            OpenChoiceMenu();

            _isSelectingProduction = true;

            item.DeleteItem();

            if (IsHoldingItem())
            {
                return GetItem();
            }

            return null;
        }

        private void Update()
        {
            if (_isSelectingProduction && Input.GetKeyDown(KeyCode.Alpha1))
            {
                CloseChoiceMenu();
                _selectedProduction = GetOutputType(1);
                _isSelectingProduction = false;
                timer.StartTimer(length);
                TriggerAnimation();
            }
            else if (_isSelectingProduction && Input.GetKeyDown(KeyCode.Alpha2))
            {
                _selectedProduction = GetOutputType(2);
                _isSelectingProduction = false;
                timer.StartTimer(length);
                TriggerAnimation();
            }
            else if (timer.IsTimeUp() && timer.IsActive())
            {
                Transform();
                timer.ResetTimer();
            }
        }

        // CHOICE MENU
        private void OpenChoiceMenu()
        {
            switch (_inputType)
            {
                case ItemType.RedPlastic:
                    redChoiceMenu.SetActive(true);
                    break;
                case ItemType.BluePlastic:
                    blueChoiceMenu.SetActive(true);
                    break;
                case ItemType.GreenPlastic:
                    greenChoiceMenu.SetActive(true);
                    break;
            }
        }

        private void CloseChoiceMenu()
        {
            redChoiceMenu.SetActive(false);
            blueChoiceMenu.SetActive(false);
            greenChoiceMenu.SetActive(false);
        }

        // TRANSFORM

        private void Transform()
        {
            var parts = itemManager.CreateItem(_selectedProduction, transform);
            HoldItem(parts);
            _selectedProduction = ItemType.None;
        }

        // UTILS

        private ItemType GetOutputType(int option)
        {
            switch (option)
            {
                case 1:
                    switch (_inputType)
                    {
                        case ItemType.RedPlastic:
                            return ItemType.RedTrainParts;
                        case ItemType.BluePlastic:
                            return ItemType.BlueTrainParts;
                        case ItemType.GreenPlastic:
                            return ItemType.GreenTrainParts;
                        default:
                            return ItemType.None;
                    }
                case 2:
                    switch (_inputType)
                    {
                        case ItemType.RedPlastic:
                            return ItemType.RedCubeParts;
                        case ItemType.BluePlastic:
                            return ItemType.BlueCubeParts;
                        case ItemType.GreenPlastic:
                            return ItemType.GreenCubeParts;
                        default:
                            return ItemType.None;
                    }
                default:
                    return ItemType.None;
            }
        }

        // ANIMATION
        private void SetAnimationColor(ItemType type)
        {
            switch (type)
            {
                case ItemType.RedPlastic:
                    _animator.SetInteger(ColorNrgb, 1);
                    break;
                case ItemType.BluePlastic:
                    _animator.SetInteger(ColorNrgb, 3);
                    break;
                case ItemType.GreenPlastic:
                    _animator.SetInteger(ColorNrgb, 2);
                    break;
            }
        }

        private void TriggerAnimation()
        {
            _animator.SetTrigger(RunMachineAnimationTrigger);
        }
    }
}
