using items;
using UnityEngine;
using managers;
using System.Collections.Generic;
using System;

namespace machines
{
    public class PaintMixer : Machine
    {
        public List<Sprite> paintSprites;
        public Timer timer;
        public ItemManager itemManager;

        private List<Item> itemsHeld = new List<Item>();
        private Item finalProduct = null;
        private int pigmentsHeld = 0;
        private int maxPigmentsHeld = 3;

        private bool paintMixtureLoaded = false; // will start the machine
        private bool timerStarted = false;

        public override void HoldItem(Item item)
        {
            bool compareRed = item.CompareTag("RedPigment");
            bool compareGreen = item.CompareTag("GreenPigment");
            bool compareBlue = item.CompareTag("BluePigment");
            bool comparePigments = compareRed || compareGreen || compareBlue;
            bool comparePaintMix = item.CompareTag("PaintMixture");

            bool pigmentHeldFull = pigmentsHeld == maxPigmentsHeld;
            //Debug.LogWarning("pigmentHeldFull: " + pigmentHeldFull);
            if (!(comparePaintMix || (comparePigments && !pigmentHeldFull))) return;

            if (comparePigments || comparePaintMix)
            {
                item.PickUp(holdSpot);
                item.IsHeldByMachine = true;

                itemHolding = item;
                itemsHeld.Add(item);

                if (comparePaintMix) 
                {
                    finalProduct = item;
                    paintMixtureLoaded = true;
                }
                else pigmentsHeld++;
            }
            //Debug.LogWarning("itemHeld:");
            //itemsHeld.ForEach(item => { Debug.Log(item.tag); });

            if (paintMixtureLoaded && pigmentsHeld > 0)
            {
                timerStarted = true;
                //timer.StartTimer(0); //instant timer
                timer.StartTimer(5);
            }
        }

        private void Update()
        {
            if (timer.IsTimeUp() && timerStarted)
            {
                itemHolding = finalProduct;
                TransformItem(itemHolding);

                timer.ResetTimer();
                timerStarted = false;
            }
        }

        public override Item TakeItemFromMachine()
        {
            var item = base.TakeItemFromMachine();
            itemsHeld.Remove(item);

            bool compareRed = item.CompareTag("RedPigment");
            bool compareGreen = item.CompareTag("GreenPigment");
            bool compareBlue = item.CompareTag("BluePigment");
            bool comparePigments = compareRed || compareGreen || compareBlue;
            bool comparePaintMix = item.CompareTag("PaintMixture");
            if (comparePaintMix)
            {
                finalProduct = null;
                paintMixtureLoaded = false;
            }
            else if (comparePigments) pigmentsHeld--;

            if (itemsHeld.Count > 0) itemHolding = itemsHeld[itemsHeld.Count - 1];

            if (!timer.IsTimeUp())
            {
                timer.ResetTimer();
            }

            return item;
        }

        private void TransformItem(Item item)
        {
            item.tag = SetPaintTag();

            switch (item.tag) {
                case "RedPaint":
                    item.SetSprite(paintSprites[0]);
                    item.type = ItemType.RedPaint;
                    break;
                case "GreenPaint":
                    item.SetSprite(paintSprites[1]);
                    item.type = ItemType.GreenPaint;
                    break;
                case "BluePaint":
                    item.SetSprite(paintSprites[2]);
                    item.type = ItemType.BluePaint;
                    break;
                case "YellowPaint":
                    item.SetSprite(paintSprites[3]);
                    item.type = ItemType.YellowPaint;
                    break;
                case "CyanPaint":
                    item.SetSprite(paintSprites[4]);
                    item.type = ItemType.CyanPaint;
                    break;
                case "PinkPaint":
                    item.SetSprite(paintSprites[5]);
                    item.type = ItemType.PinkPaint;
                    break;
                case "OrangePaint":
                    item.SetSprite(paintSprites[6]);
                    item.type = ItemType.OrangePaint;
                    break;
                case "PurplePaint":
                    item.SetSprite(paintSprites[7]);
                    item.type = ItemType.PurplePaint;
                    break;
                default:
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
            
            paintMixtureLoaded = false;
            finalProduct = null;
            pigmentsHeld = 0;
        }

        private string SetPaintTag()
        {
            string paintTag = string.Empty;

            int countRedPigment = 0;
            int countGreenPigment = 0;
            int countBluePigment = 0;
            foreach (var item in itemsHeld)
            {
                if (item.CompareTag("RedPigment"))
                {
                    countRedPigment++;
                } else if (item.CompareTag("GreenPigment"))
                {
                    countGreenPigment++;
                } else if (item.CompareTag("BluePigment"))
                {
                    countBluePigment++;
                }
            }

            //red + green = yellow
            //green + blue = cyan
            //red + blue = pink
            //red + red + green = orange
            //green + blue + blue = purple
            if (countRedPigment == 1 && countGreenPigment == 1) paintTag = "YellowPaint";
            else if (countGreenPigment == 1 && countBluePigment == 1) paintTag = "CyanPaint";
            else if (countRedPigment == 1 && countBluePigment == 1) paintTag = "PinkPaint";
            else if (countRedPigment == 2 && countGreenPigment == 1) paintTag = "OrangePaint";
            else if (countGreenPigment == 1 && countBluePigment == 2) paintTag = "PurplePaint";
            else if (countRedPigment != 0) paintTag = "RedPaint";
            else if (countGreenPigment != 0) paintTag = "GreenPaint";
            else if (countBluePigment != 0) paintTag = "BluePaint";

            return paintTag;
        }
    }
}
