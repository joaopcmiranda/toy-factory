using items;
using UnityEngine;
using managers;
using System.Collections.Generic;

namespace machines
{
    public class Level4_Machines : Machine
    {
        public ItemManager itemManager;
        public Level4_minigame miniGame;
        public List<Item> requiredItems = new List<Item>();
        public Item outputItem;
        public bool requireSecondItem;
        public bool requiredFulfilled; 
        public int gameOption;
        
        private List<Item> itemsHeld = new List<Item>();

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

        public override void HoldItem(Item item)
        {
            item.PickUp(holdSpot);
            item.IsHeldByMachine = true;

            itemHolding = item;
            itemsHeld.Add(item);

            requiredFulfilled = hasRequiredItems(); 

            if(requiredFulfilled)
            {
                StartManualAssembly();
            } else
            {
                StopManualAssembly();
            }
        }

        public override Item TakeItemFromMachine()
        {
            StopManualAssembly(); 
            var item = base.TakeItemFromMachine();
            itemsHeld.Remove(item);

            requiredFulfilled = hasRequiredItems();



            if (itemsHeld.Count > 0) itemHolding = itemsHeld[itemsHeld.Count - 1];

            return item;
        }

        public void TransformItem(Item item)
        {
            item.tag = outputItem.type.ToString();
            item.SetSprite(outputItem.getSprite());
            item.type = outputItem.type;

            for (int i = (itemsHeld.Count - 1); i >= 0; i--)
            {
                if (itemsHeld[i] != item)
                {
                    itemsHeld[i].DeleteItem();
                    itemsHeld.RemoveAt(i);
                }
            }

            itemManager.RefreshItems();
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
            TransformItem(itemHolding);
        }

        public Item getItem()
        {
            return itemsHeld[0];
        }
    }
}
