using items;
using UnityEngine;
using items.handling;
using stations.minigames;
using System;

namespace stations
{
    public class Assembly : ItemHolder
    {
        public bool handAssembly;
        public bool tutorial;

        private bool _isHoldingCarriageParts;
        private bool _isHoldingParts;
        private bool _isHoldingWheels;
        private bool isHoldingTrainItems
        {
            get => _isHoldingParts && _isHoldingWheels;
        }

        private AssemblyMiniGame _assemblyMiniGame;
        private AssemblyTutorial _assemblyTutorial;
        public Timer timer;

        private ItemType _trainPartsType;

        public override Item GetItem()
        {
            var item = ReleaseLastItem();

            if (item.type == ItemType.Wheels)
            {
                _isHoldingWheels = false;
            }
            else
            {
                _trainPartsType = default;
                _isHoldingParts = false;
            }

            return item;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;

            if (item.type == ItemType.Wheels)
            {
                _isHoldingWheels = true;
            }
            else
            {
                _isHoldingParts = true;
                _trainPartsType = item.type;
                _isHoldingCarriageParts = item.type.ToString().Contains("Carriage");
            }

            var returnItem = HoldItem(item);

            if (isHoldingTrainItems)
            {
                if (handAssembly)
                {
                    ManualAssembly();
                }
                else
                {
                    timer.StartTimer(5);
                }
            }

            return returnItem;
        }

        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                //train parts
                case ItemType.PaintedTrainParts:
                case ItemType.RedTrainParts:
                case ItemType.GreenTrainParts:
                case ItemType.BlueTrainParts:
                case ItemType.YellowTrainParts:
                case ItemType.CyanTrainParts:
                case ItemType.PinkTrainParts:
                case ItemType.OrangeTrainParts:
                case ItemType.PurpleTrainParts:
                //carriage parts
                case ItemType.RedCarriageParts:
                case ItemType.GreenCarriageParts:
                case ItemType.BlueCarriageParts:
                case ItemType.YellowCarriageParts:
                case ItemType.CyanCarriageParts:
                case ItemType.PinkCarriageParts:
                case ItemType.OrangeCarriageParts:
                case ItemType.PurpleCarriageParts:
                    return !_isHoldingParts;
                case ItemType.Wheels:
                    return !_isHoldingWheels;
                default:
                    return false;
            }
        }


        public override void Start()
        {
            base.Start();
            if (handAssembly) //hand assembly: minigame
            {
                _assemblyMiniGame = FindObjectOfType<AssemblyMiniGame>();
                if (_assemblyMiniGame == null)
                {
                    Debug.LogError("No AssemblyMiniGame found in the scene, but handAssembly is set to true.");
                }
                if (tutorial)
                {
                    _assemblyTutorial = FindObjectOfType<AssemblyTutorial>();
                    if (_assemblyTutorial == null)
                    {
                        Debug.LogError("No AssemblyTutorial found in the scene, but handAssembly is set to true.");
                    }
                }

            }
            /*else //non hand assembly: timer
            {
                GameObject.FindWithTag("Assembly").GetComponent<Timer>();
            }*/
        }


        private void Update()
        {
            if (!handAssembly)
            {
                if (timer.IsActive() && timer.IsTimeUp() && isHoldingTrainItems)
                {
                    Transform();
                    timer.ResetTimer();
                }
            }
        }


        public void Transform()
        {
            var type = GetTrainType();
            var newItem = itemManager.CreateItem(type, transform);

            DestroyAllItems();

            _isHoldingParts = false;
            _isHoldingWheels = false;

            HoldItem(newItem);
        }

        private ItemType GetTrainType()
        {
            string typeName = _trainPartsType.ToString();
            int colorNameEndPos = typeName.IndexOf("TrainParts");
            if (colorNameEndPos == -1)
            {
                colorNameEndPos = typeName.IndexOf("CarriageParts");
            }

            if (colorNameEndPos > -1)
            {
                string colorName = typeName.Substring(0, colorNameEndPos);
                if (_isHoldingCarriageParts)
                {
                    return (ItemType)Enum.Parse(typeof(ItemType), colorName + "Carriage");
                }
                else
                {
                    return (ItemType)Enum.Parse(typeof(ItemType), colorName + "Train");
                }
            }
            return _isHoldingCarriageParts ? ItemType.Carriage : ItemType.Train;
        }


        private void ManualAssembly()
        {
            _assemblyMiniGame.StartGame();
            if (tutorial)
            {
                _assemblyTutorial.StartTutorial();
            }
        }

        public void BreakItems()
        {
            if (GetHeldItemCount() > 1)
            {
                DestroyAllItems();

                _isHoldingParts = false;
                _isHoldingWheels = false;
            }
        }
    }
}
