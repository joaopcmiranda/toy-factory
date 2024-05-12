using items;
using items.handling;
using UnityEngine;

namespace stations
{
    public class MetalPress : ItemHolder
    {
        public int length = 2;

        public Timer timer;

        private Animator _animator;
        private static readonly int RunMachineAnimationTrigger = Animator.StringToHash("Run Machine");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override bool CanReceiveItem(Item item)
        {
            return item.type == ItemType.MetalSheet;
        }

        public override Item GetItem()
        {
            if (IsHoldingItem())
            {
                return ReleaseLastItem();
            }

            return null;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;
            item.DeleteItem();

            timer.StartTimer(length);
            TriggerAnimation();
            return null;
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
            var parts = itemManager.CreateItem(ItemType.MetalTrainBody, transform);
            HoldItem(parts);
        }

        // ANIMATION
        private void TriggerAnimation()
        {
            _animator.SetTrigger(RunMachineAnimationTrigger);
        }
    }
}
