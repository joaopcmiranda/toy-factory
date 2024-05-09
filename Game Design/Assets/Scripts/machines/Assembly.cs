using items;
using UnityEngine;
using managers;
using System.Collections.Generic;

namespace machines
{
    public class Assembly : Machine
    {
        public List<Sprite> trainSprites;
        public ItemManager itemManager;
        public bool handAssembly;       

        private List<Item> itemsHeld = new List<Item>();
        private Item finalProduct = null; //item that the machine produces for consistent scale

        private bool _isHoldingParts;
        private bool _isHoldingWheels;
        private bool _isHoldingTrainItems
        {
            get
            {
                return _isHoldingParts && _isHoldingWheels;
            }
        }

        private AssemblyMiniGame assemblyMiniGame;
        private AssemblyTutorial assemblyTutorial;
        public Timer timer;

        private string trainPartsTag = string.Empty;

        public override void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();

            if (handAssembly) //hand assembly: minigame
            {
                assemblyMiniGame = FindObjectOfType<AssemblyMiniGame>();
                if (assemblyMiniGame == null)
                {
                    Debug.LogError("No AssemblyMiniGame found in the scene, but handAssembly is set to true.");
                }
                assemblyTutorial = FindObjectOfType<AssemblyTutorial>();
                if (assemblyTutorial == null)
                {
                    Debug.LogError("No AssemblyTutorial found in the scene, but handAssembly is set to true.");
                }
            }
            /*else //non hand assembly: timer
            {
                GameObject.FindWithTag("Assembly").GetComponent<Timer>();
            }*/
        }

        public override void HoldItem(Item item)
        {
            bool comparePaintedParts = item.CompareTag("TrainPartsPainted");
            bool compareRedParts = item.CompareTag("RedTrainParts");
            bool compareGreenParts = item.CompareTag("GreenTrainParts");
            bool compareBlueParts = item.CompareTag("BlueTrainParts");
            bool compareYellowParts = item.CompareTag("YellowTrainParts");
            bool compareCyanParts = item.CompareTag("CyanTrainParts");
            bool comparePinkParts = item.CompareTag("PinkTrainParts");
            bool compareOrangeParts = item.CompareTag("OrangeTrainParts");
            bool comparePurpleParts = item.CompareTag("PurpleTrainParts");
            bool compareTrainParts = comparePaintedParts || compareRedParts || compareGreenParts || compareBlueParts || compareYellowParts || compareCyanParts || comparePinkParts || compareOrangeParts || comparePurpleParts;
            
            bool compareWheels = item.CompareTag("TrainWheels");
            
            if (!((compareTrainParts && !_isHoldingParts) || (compareWheels && !_isHoldingWheels))) return;

            if (compareTrainParts || compareWheels)
            {
                item.PickUp(holdSpot);
                item.IsHeldByMachine = true;

                itemHolding = item;
                itemsHeld.Add(item);

                if (compareTrainParts)
                {
                    finalProduct = item;
                    trainPartsTag = item.tag;
                    _isHoldingParts = true;
                }
                else
                {
                    _isHoldingWheels = true;
                }
            }


            if (_isHoldingTrainItems)
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
        }

        private void Update()
        {
            if (!handAssembly)
            {
                if (timer.IsTimeUp() && _isHoldingTrainItems)
                {
                    FinishAssembly();
                    timer.ResetTimer();
                }
            }
        }

        public override Item TakeItemFromMachine()
        {
            var item = base.TakeItemFromMachine();
            itemsHeld.Remove(item);

            if (item.CompareTag("TrainWheels"))
            {
                _isHoldingWheels = false;
            }
            else
            {
                finalProduct = null;
                trainPartsTag = string.Empty;
                _isHoldingParts = false;
            }

            if (itemsHeld.Count > 0) itemHolding = itemsHeld[itemsHeld.Count - 1];

            return item;
        }

        private void TransformItem(Item item)
        {
            item.tag = SetTrainTag();

            switch (item.tag)
            {
                case "RedTrain":
                    item.SetSprite(trainSprites[1]);
                    item.type = ItemType.RedTrain;
                    break;
                case "GreenTrain":
                    item.SetSprite(trainSprites[2]);
                    item.type = ItemType.GreenTrain;
                    break;
                case "BlueTrain":
                    item.SetSprite(trainSprites[3]);
                    item.type = ItemType.BlueTrain;
                    break;
                case "YellowTrain":
                    item.SetSprite(trainSprites[4]);
                    item.type = ItemType.YellowTrain;
                    break;
                case "CyanTrain":
                    item.SetSprite(trainSprites[5]);
                    item.type = ItemType.CyanTrain;
                    break;
                case "PinkTrain":
                    item.SetSprite(trainSprites[6]);
                    item.type = ItemType.PinkTrain;
                    break;
                case "OrangeTrain":
                    item.SetSprite(trainSprites[7]);
                    item.type = ItemType.OrangeTrain;
                    break;
                case "PurpleTrain":
                    item.SetSprite(trainSprites[8]);
                    item.type = ItemType.PurpleTrain;
                    break;
                default:
                    item.SetSprite(trainSprites[0]);
                    item.type = ItemType.Train;
                    break;
            }

            for (int i = (itemsHeld.Count - 1); i >= 0; i--)
            {
                if (itemsHeld[i] != item)
                {
                    itemsHeld[i].DeleteItem();
                    itemsHeld.RemoveAt(i);
                }
            }

            itemManager.RefreshItems();

            _isHoldingParts = false;
            _isHoldingWheels = false;
        }

        private string SetTrainTag()
        {
            string trainTag = string.Empty;
            switch (trainPartsTag)
            {
                case "RedTrainParts":
                    trainTag = "RedTrain";
                    break;
                case "GreenTrainParts":
                    trainTag = "GreenTrain";
                    break;
                case "BlueTrainParts":
                    trainTag = "BlueTrain";
                    break;
                case "YellowTrainParts":
                    trainTag = "YellowTrain";
                    break;
                case "CyanTrainParts":
                    trainTag = "CyanTrain";
                    break;
                case "PinkTrainParts":
                    trainTag = "PinkTrain";
                    break;
                case "OrangeTrainParts":
                    trainTag = "OrangeTrain";
                    break;
                case "PurpleTrainParts":
                    trainTag = "PurpleTrain";
                    break;
                default:
                    trainTag = "Train";
                    break;
            }

            return trainTag;
        }

        private void ManualAssembly()
        {
            assemblyMiniGame.StartGame();
            assemblyTutorial.StartTutorial();
        }

        public void BreakItems()
        {
            if (itemsHeld.Count > 1)
            {
                for (int i = (itemsHeld.Count - 1); i >= 0; i--)
                {
                    itemsHeld[i].DeleteItem();
                    itemsHeld.RemoveAt(i);
                }

                itemManager.RefreshItems();

                _isHoldingParts = false;
                _isHoldingWheels = false;
            }          
        }

        public void FinishAssembly()
        {
            TransformItem(itemHolding);
        }
    }
}
