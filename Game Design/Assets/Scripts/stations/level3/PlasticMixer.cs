using items;
using items.handling;
using managers;

namespace stations
{
    public class PlasticMixer : ItemHolder
    {
        public Timer timer;

        private ItemType _pigment;
        private bool _isHoldingPigment;

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
            if (IsHoldingItem())
            {
                timer.ResetTimer();
                return ReleaseLastItem();
            }
            return null;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;

            if (!IsHoldingItem() && !_isHoldingPigment) // Machine empty and has not mixed anything
            {
                if (IsPigment(item)) // Apply the pigment
                {
                    SetPigment(item);
                    return null;
                }
                else // Hold the plastic chips until pigment is applied
                {
                    return HoldPlasticChips(item);
                }
            }
            else if (IsHoldingItem() && itemsHeld[0].type == ItemType.PlasticChips) // Machine has chips but no pigment
            {
                if (IsPigment(item)) // Apply the pigment and start mixing
                {
                    SetPigment(item);
                    StartMixing();
                    return null;
                }
                else // Swap the chips in case this was unintentional to prevent them from falling on the floor
                {
                    return HoldPlasticChips(item);
                }
            }
            else if (IsHoldingItem() && IsOutput(itemsHeld[0])) // Machine has finished mixing, will produce more once it's emptied. Has Pigment
            {
                if (IsPigment(item)) // Apply the new pigment
                {
                    SetPigment(item);
                }
                else // Ignore the new chips since this will indefinitely produce
                {
                    item.DeleteItem();
                }
                return GetItem(); // GetItem also restarts the mixing process
            }
            else if (_isHoldingPigment) // Machine has pigment but no chips or output
            {
                if (IsPigment(item)) // Apply the new pigment
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
            return item;
        }

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

        private Item HoldPlasticChips(Item item)
        {
            if (!_isHoldingPigment)
            {
                HoldItem(item);
                return null;
            }
            return HoldItem(item);
        }

        private void SetPigment(Item item)
        {
            _pigment = item.type;
            _isHoldingPigment = true;
            return null;
        }

        private void StartMixing()
        {
            timer.StartTimer(5);
        }

        private void Update()
        {
            if (timer.IsTimeUp() && timer.IsActive())
            {
                Transform();
                timer.ResetTimer();
            }
        }

        private void Transform()
        {
            ReleaseLastItem()
                ?.DeleteItem();

            ItemType outputType;
            //you will need to start from the Game scene
            switch (_levelManager.GetLevelScene())
            {
                case 1:
                    outputType = ItemType.UnpaintedTrainParts;
                    break;
                default:
                    outputType = ItemType.PaintedTrainParts;
                    break;
            }
            var parts = itemManager.CreateItem(outputType, transform);
            HoldItem(parts);
        }
    }
}
