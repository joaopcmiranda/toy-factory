using items;
using UnityEngine;
using managers;
using System.Collections.Generic;
using System;
using System.Collections;

namespace machines
{
    public class PaintColorStation : Machine
    {
        public List<Sprite> trainPartsColorSprites;
        public List<Sprite> paintStationColorSprites;
        public Timer timer;
        public ItemManager itemManager;

        private List<Item> itemsHeld = new List<Item>();
        private Item finalProduct = null; //item that the machine produces for consistent scale
        private string paintTag = string.Empty;
        private bool paintHeld = false;
        private bool trainPartsHeld = false;

        private bool timerStarted = false;

        public override void HoldItem(Item item)
        {
            bool compareRedPaint = item.CompareTag("RedPaint");
            bool compareGreenPaint = item.CompareTag("GreenPaint");
            bool compareBluePaint = item.CompareTag("BluePaint");
            bool compareYellowPaint = item.CompareTag("YellowPaint");
            bool compareCyanPaint = item.CompareTag("CyanPaint");
            bool comparePinkPaint = item.CompareTag("PinkPaint");
            bool compareOrangePaint = item.CompareTag("OrangePaint");
            bool comparePurplePaint = item.CompareTag("PurplePaint");
            bool comparePaint = compareRedPaint || compareGreenPaint || compareBluePaint || compareYellowPaint || compareCyanPaint || comparePinkPaint || compareOrangePaint || comparePurplePaint;
            
            bool compareTrainParts = item.CompareTag("TrainPartsUnpainted");

            if (!((comparePaint && !paintHeld) || (compareTrainParts && !trainPartsHeld))) return;

            if (comparePaint || compareTrainParts)
            {
                item.PickUp(holdSpot);
                item.IsHeldByMachine = true;

                itemHolding = item;

                if (compareTrainParts) 
                {
                    finalProduct = item;
                    trainPartsHeld = true;
                    itemsHeld.Add(item);
                }
                else 
                {
                    paintHeld = true;
                    paintTag = item.tag;
                    SetPaintStationColor(paintTag);
                    item.DeleteItem();
                }
            }

            if (paintHeld && trainPartsHeld)
            {
                timerStarted = true;
                timer.StartTimer(0); //instant timer
                //timer.StartTimer(5);
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

            if (item.CompareTag("TrainPartsUnpainted"))
            {
                finalProduct = null;
                trainPartsHeld = false;
            }
            else paintHeld = false;

            if (itemsHeld.Count > 0) itemHolding = itemsHeld[itemsHeld.Count - 1];

            if (!timer.IsTimeUp())
            {
                timer.ResetTimer();
            }

            return item;
        }

        private void TransformItem(Item item)
        {
            item.tag = SetTrainPartsTag();

            switch (item.tag) {
                case "RedTrainParts":
                    item.SetSprite(trainPartsColorSprites[0]);
                    item.type = ItemType.RedTrainParts;
                    break;
                case "GreenTrainParts":
                    item.SetSprite(trainPartsColorSprites[1]);
                    item.type = ItemType.GreenTrainParts;
                    break;
                case "BlueTrainParts":
                    item.SetSprite(trainPartsColorSprites[2]);
                    item.type = ItemType.BlueTrainParts;
                    break;
                case "YellowTrainParts":
                    item.SetSprite(trainPartsColorSprites[3]);
                    item.type = ItemType.YellowTrainParts;
                    break;
                case "CyanTrainParts":
                    item.SetSprite(trainPartsColorSprites[4]);
                    item.type = ItemType.CyanTrainParts;
                    break;
                case "PinkTrainParts":
                    item.SetSprite(trainPartsColorSprites[5]);
                    item.type = ItemType.PinkTrainParts;
                    break;
                case "OrangeTrainParts":
                    item.SetSprite(trainPartsColorSprites[6]);
                    item.type = ItemType.OrangeTrainParts;
                    break;
                case "PurpleTrainParts":
                    item.SetSprite(trainPartsColorSprites[7]);
                    item.type = ItemType.PurpleTrainParts;
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

            SetPaintStationColor(string.Empty);

            finalProduct = null;
            paintTag = string.Empty;
            paintHeld = false;
            trainPartsHeld = false;
        }

        private string SetTrainPartsTag()
        {
            string ColorTrainPartsTag = string.Empty;
            switch (paintTag)
            {
                case "RedPaint":
                    ColorTrainPartsTag = "RedTrainParts";
                    break;
                case "GreenPaint":
                    ColorTrainPartsTag = "GreenTrainParts";
                    break;
                case "BluePaint":
                    ColorTrainPartsTag = "BlueTrainParts";
                    break;
                case "YellowPaint":
                    ColorTrainPartsTag = "YellowTrainParts";
                    break;
                case "CyanPaint":
                    ColorTrainPartsTag = "CyanTrainParts";
                    break;
                case "PinkPaint":
                    ColorTrainPartsTag = "PinkTrainParts";
                    break;
                case "OrangePaint":
                    ColorTrainPartsTag = "OrangeTrainParts";
                    break;
                case "PurplePaint":
                    ColorTrainPartsTag = "PurpleTrainParts";
                    break;
                default:
                    break;
            }

            return ColorTrainPartsTag;
        }

        private void SetPaintStationColor(string itemTag)
        {
            switch (itemTag)
            {
                case "RedPaint":
                    spriteRenderer.sprite = paintStationColorSprites[0];
                    break;
                case "GreenPaint":
                    spriteRenderer.sprite = paintStationColorSprites[1];
                    break;
                case "BluePaint":
                    spriteRenderer.sprite = paintStationColorSprites[2];
                    break;
                case "YellowPaint":
                    spriteRenderer.sprite = paintStationColorSprites[3];
                    break;
                case "CyanPaint":
                    spriteRenderer.sprite = paintStationColorSprites[4];
                    break;
                case "PinkPaint":
                    spriteRenderer.sprite = paintStationColorSprites[5];
                    break;
                case "OrangePaint":
                    spriteRenderer.sprite = paintStationColorSprites[6];
                    break;
                case "PurplePaint":
                    spriteRenderer.sprite = paintStationColorSprites[7];
                    break;
                default:
                    spriteRenderer.sprite = paintStationColorSprites[8];
                    break;
            }
        }
    }
}
