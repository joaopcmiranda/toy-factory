using System;
using items;
using items.handling;
using UnityEngine;
using UnityEngine.Serialization;

namespace stations
{
    public class LaserCutter : ItemHolder
    {

        public int length = 5;
        public Timer timer;
        public GameObject choiceMenu;

        private Animator _animator;
        private static readonly int RunMachineAnimationTrigger = Animator.StringToHash("Run Machine");

        private ItemType _selectedProduction;
        private bool _isSelectingProduction;

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
            if (IsHoldingItem() && !_isSelectingProduction)
            {
                return ReleaseLastItem();
            }

            return null;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item) || timer.IsActive()) return item;

            _isSelectingProduction = true;

            OpenChoiceMenu();

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
                _selectedProduction = ItemType.Wheels;
                _isSelectingProduction = false;
                timer.StartTimer(length);
                TriggerAnimation();
            }
            else if (_isSelectingProduction && Input.GetKeyDown(KeyCode.Alpha2))
            {
                _selectedProduction = ItemType.Slinky;
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
            choiceMenu.SetActive(true);
        }
        private void CloseChoiceMenu()
        {
            choiceMenu.SetActive(false);
        }

        // TRANSFORM
        private void Transform()
        {
            var parts = itemManager.CreateItem(_selectedProduction, transform);
            HoldItem(parts);
            _selectedProduction = ItemType.None;
        }

        // ANIMATION
        private void TriggerAnimation()
        {
            _animator.SetTrigger(RunMachineAnimationTrigger);
        }
    }
}
