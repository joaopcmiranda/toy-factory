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
        public int gameOption;
        
        private List<Item> itemsHeld = new List<Item>();

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

            if(item.type == requiredItems[0].type)
            {
                StartManualAssembly();
            }
        }

        public override Item TakeItemFromMachine()
        {
            StopManualAssembly(); 
            var item = base.TakeItemFromMachine();
            itemsHeld.Remove(item);

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
