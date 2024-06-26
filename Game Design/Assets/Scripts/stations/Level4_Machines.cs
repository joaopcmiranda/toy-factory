using items;
using UnityEngine;
using managers;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace machines
{
    public class Machine_Level4 : Machine_Base_Level4
    {
        public ItemManager_Level4 itemManager;
        public Level4_minigame miniGame;
        public List<Item_Level4> requiredItems = new List<Item_Level4>();
        public Item_Level4 outputItem;
        public bool requiredFulfilled;
        public bool requiresMultiple;
        public int gameOption;

        public List<Item_Level4> itemsHeld = new List<Item_Level4>();

        public bool hasRequiredItems()
        {
            // Iterate through each required item
            foreach (var requiredItem in requiredItems)
            {

                // Check if the required item is not in itemsHeld
                bool found = false;
                foreach (var heldItem in itemsHeld)
                {

                    if (requiredItem.type == heldItem.type)
                    {
                        found = true;
                        break;
                    }
                }

                // If any required item is not found in itemsHeld, return false
                if (!found)
                {
                    return false;
                }
            }
            // If all required items are found in itemsHeld, return true
            return true;

        }

        public override void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        }

        public override void HoldItem(Item_Level4 item)
        {
            Debug.Log("HoldItem");
            item.PickUp(holdSpot);
            item.IsHeldByMachine = true;

            itemHolding = item;
            itemsHeld.Add(item);

            requiredFulfilled = hasRequiredItems();

            if (requiredFulfilled)
            {
                StartManualAssembly();
            }
            else
            {
                StopManualAssembly();
            }
        }

        public override Item_Level4 TakeItemFromMachine()
        {
            Debug.Log("TakeItemFromMachine");
            StopManualAssembly();
            var item = base.TakeItemFromMachine();
            itemsHeld.Remove(item);

            requiredFulfilled = hasRequiredItems();

            if (itemsHeld.Count > 0) itemHolding = itemsHeld[itemsHeld.Count - 1];

            return item;
        }

        public void TransformItem(Item_Level4 item)
        {
            Debug.Log("TransformItem");
            itemsHeld[0].tag = outputItem.type.ToString();

            //Set sprite 
            //Set sprite size 

            itemsHeld[0].SetSprite(outputItem.getSpriteRenderer().sprite);
            itemsHeld[0].setItemSize(new Vector3(2f, 2f, 2f));

            itemsHeld[0].type = outputItem.type;


            if (itemsHeld.Count > 1)
            {
                for (int i = (itemsHeld.Count - 1); i > 0; i--)
                {
                    itemsHeld[i].DeleteItem();
                    itemsHeld.RemoveAt(i);
                }

                itemManager.RefreshItems();
                itemsHeld[0].PickUp(holdSpot);
                itemsHeld[0].IsHeldByMachine = true;

                itemHolding = item;
                //itemsHeld.Add(item);
                //HoldItem(itemsHeld[0]);

            }
        }

        private void StartManualAssembly()
        {
            miniGame.StartGame();
        }

        private void StopManualAssembly()
        {
            miniGame.StopGame();
        }

        public void BreakItems()
        {
            Debug.Log("BreakItems");
            if (itemsHeld.Count > 1)
            {
                for (int i = (itemsHeld.Count - 1); i >= 0; i--)
                {
                    itemsHeld[i].DeleteItem();
                    itemsHeld.RemoveAt(i);
                }

                itemManager.RefreshItems();

            }
        }

        public void FinishAssembly()
        {
            Debug.Log("FinishAssembly");
            TransformItem(itemHolding);
        }

        public Item_Level4 getItem()
        {
            return itemsHeld[0];
        }
    }
}
