using items;
using UnityEngine;
using System.Collections.Generic;
using items.handling;

namespace stations
{
    public class PaintMixer : ItemHolder
    {
        public SpriteRenderer spriteRenderer;

        public List<Sprite> paintMixerSprites;
        public Timer timer;

        private bool _paintMixtureLoaded; // will start the machine

        public override Item GetItem()
        {
            timer.ResetTimer();
            return ReleaseLastItem();
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;
            Item returnItem = null;
            switch (item.type)
            {
                case ItemType.RedPigment:
                case ItemType.GreenPigment:
                case ItemType.BluePigment:
                    returnItem = HoldItem(item);
                    break;
                case ItemType.PaintMixture:
                    _paintMixtureLoaded = true;
                    item.DeleteItem();
                    break;
            }
            if (_paintMixtureLoaded && GetHeldItemCount() > 0)
            {
                timer.StartTimer(5);
                spriteRenderer.sprite = paintMixerSprites[1];
            }

            return returnItem;
        }

        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.RedPigment:
                case ItemType.GreenPigment:
                case ItemType.BluePigment:
                    return true;
                case ItemType.PaintMixture:
                    return true;
                default:
                    return false;
            }
        }

        private void Update()
        {
            if (timer.IsTimeUp() && timer.IsActive())
            {
                Transform();

                timer.ResetTimer();

                spriteRenderer.sprite = paintMixerSprites[0];
            }
        }

        private void Transform()
        {
            Item item = itemManager.CreateItem(GetPaintColor(), transform);

            DestroyAllItems();

            _paintMixtureLoaded = false;
            HoldItem(item);
        }

        private ItemType GetPaintColor()
        {

            var countRedPigment = 0;
            var countGreenPigment = 0;
            var countBluePigment = 0;
            foreach (var item in itemsHeld)
            {
                if (item.CompareTag("RedPigment"))
                {
                    countRedPigment++;
                }
                else if (item.CompareTag("GreenPigment"))
                {
                    countGreenPigment++;
                }
                else if (item.CompareTag("BluePigment"))
                {
                    countBluePigment++;
                }
            }

            //red + green = yellow
            //green + blue = cyan
            //red + blue = pink
            //red + red + green = orange
            //green + blue + blue = purple
            if (countRedPigment == 1 && countGreenPigment == 1) return ItemType.YellowPaint;
            else if (countGreenPigment == 1 && countBluePigment == 1) return ItemType.CyanPaint;
            else if (countRedPigment == 1 && countBluePigment == 1) return ItemType.PinkPaint;
            else if (countRedPigment == 2 && countGreenPigment == 1) return ItemType.OrangePaint;
            else if (countGreenPigment == 1 && countBluePigment == 2) return ItemType.PurplePaint;
            else if (countRedPigment != 0) return ItemType.RedPaint;
            else if (countGreenPigment != 0) return ItemType.GreenPaint;
            else if (countBluePigment != 0) return ItemType.BluePaint;

            return ItemType.BluePaint;
        }
    }
}
