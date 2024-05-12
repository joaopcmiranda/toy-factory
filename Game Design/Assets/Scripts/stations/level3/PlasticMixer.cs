using items;
using items.handling;
using managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace stations
{
    public class PlasticMixer : ItemHolder
    {
        public int length = 2;
        public int penaltyForChangingPigment = 1;

        private ItemType _pigment;
        private bool _isHoldingPigment;

        private Timer _timer;
        private Animator _animator;
        private static readonly int MachineRunning = Animator.StringToHash("Running");
        private static readonly int ColorNrgb = Animator.StringToHash("ColorNRGB");

        // LIFE CYCLE

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _timer = GetComponent<Timer>();
        }

        private void Update()
        {
            if (_timer.IsTimeUp() && _timer.IsActive())
            {
                GeneratePlastic();
                _timer.ResetTimer();
                StopAnimation();
            }
        }

        // IItemHolder

        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.PlasticChips:
                case ItemType.RedPigment:
                case ItemType.BluePigment:
                case ItemType.GreenPigment:
                    return true;
                default:
                    return false;
            }
        }

        public override Item GetItem()
        {
            var item = ReleaseLastItem();
            if (IsHoldingItem())
            {
                if (IsOutput(item))
                {
                    _timer.ResetTimer();
                    StartMixing(); // restart machine to produce more
                }
                return item;
            }
            return null;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;

            var isPigment = IsPigment(item);
            var isMachineHoldingChips = IsHoldingItem() && itemsHeld[0].type == ItemType.PlasticChips;
            var isMachineHoldingOutput = IsHoldingItem() && IsOutput(itemsHeld[0]);

            if (_timer.IsActive()) // Machine is mixing
            {
                if (isPigment) // Reset mixing with time penalty
                {
                    _timer.ResetTimer();
                    StartMixing(penaltyForChangingPigment);
                    return item;
                }
                else // Reject chips while mixing
                {
                    return item;
                }
            }
            else if (isMachineHoldingOutput) // Machine has finished mixing, will produce more once it's emptied. Has Pigment
            {
                if (isPigment) // Apply the new pigment
                {
                    SetPigment(item);
                }
                else // Ignore the new chips since this will indefinitely produce
                {
                    item.DeleteItem();
                }
                return GetItem(); // GetItem also restarts the mixing process
            }
            else if (isMachineHoldingChips) // Machine has chips but no pigment
            {
                if (isPigment) // Apply the pigment and start mixing
                {
                    SetPigment(item);
                    ReleaseLastItem().DeleteItem();
                    StartMixing();
                    return null;
                }
                else // Swap the chips in case this was unintentional to prevent them from falling on the floor
                {
                    return HoldPlasticChips(item);
                }
            }
            else if (_isHoldingPigment) // Machine has pigment but no chips or output
            {
                if (isPigment) // Apply the new pigment
                {
                    SetPigment(item);
                    return null;
                }
                else // Receive the chips and start mixing
                {
                    HoldPlasticChips(item);
                    StartMixing();
                }
            }
            else // Machine empty and has not mixed anything
            {
                if (isPigment) // Apply the pigment
                {
                    SetPigment(item);
                    return null;
                }
                else // Hold the plastic chips until pigment is applied
                {
                    return HoldPlasticChips(item);
                }
            }
            return item;
        }

        // OPERATIONS

        private Item HoldPlasticChips(Item item)
        {
            if (_isHoldingPigment)
            {
                item.DeleteItem();
                return null;
            }
            return HoldItem(item);
        }

        private void SetPigment(Item item)
        {
            _pigment = item.type;
            _isHoldingPigment = true;
            SetAnimationColor(_pigment);
            item.DeleteItem();
        }

        private void StartMixing(int penalty = 0)
        {
            _timer.StartTimer(length + penalty);
            StartAnimation();
        }

        private void GeneratePlastic()
        {
            var plastic = itemManager.CreateItem(outputType, transform);
            HoldItem(plastic);
        }

        // UTILS

        private bool IsPigment(Item item)
        {
            switch (item.type)
            {
                case ItemType.RedPigment:
                case ItemType.BluePigment:
                case ItemType.GreenPigment:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsOutput(Item item)
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

        private ItemType outputType
        {
            get
            {
                switch (_pigment)
                {
                    case ItemType.RedPigment:
                        return ItemType.RedPlastic;
                    case ItemType.BluePigment:
                        return ItemType.BluePlastic;
                    case ItemType.GreenPigment:
                        return ItemType.GreenPlastic;
                    default:
                        return ItemType.RedPlastic;
                }
            }
        }

        // ANIMATION
        private void SetAnimationColor(ItemType type)
        {
            switch (type)
            {
                case ItemType.RedPigment:
                    _animator.SetInteger(ColorNrgb, 1);
                    break;
                case ItemType.GreenPigment:
                    _animator.SetInteger(ColorNrgb, 2);
                    break;
                case ItemType.BluePigment:
                    _animator.SetInteger(ColorNrgb, 3);
                    break;
            }
        }

        private void StartAnimation()
        {
            _animator.SetBool(MachineRunning, true);
        }

        private void StopAnimation()
        {
            _animator.SetBool(MachineRunning, false);
        }
    }
}
