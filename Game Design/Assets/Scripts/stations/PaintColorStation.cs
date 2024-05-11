using items;
using UnityEngine;
using System.Collections.Generic;
using items.handling;

namespace stations
{
    public class PaintColorStation : ItemHolder
    {
        public List<Sprite> paintStationColorSprites;
        public Timer timer;

        private ItemType _paintType;
        private bool _paintHeld;
        private bool _trainPartsHeld;

        private SpriteRenderer _spriteRenderer;

        public override void Start()
        {
            base.Start();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override Item GetItem()
        {
            var item = ReleaseLastItem();

            if (item && item.type == ItemType.UnpaintedTrainParts)
            {
                _trainPartsHeld = false;
            }

            if (!timer.IsTimeUp())
            {
                timer.ResetTimer();
            }

            return item;
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;
            Item returnItem = null;
            switch (item.type)
            {
                case ItemType.RedPaint:
                case ItemType.GreenPaint:
                case ItemType.BluePaint:
                case ItemType.YellowPaint:
                case ItemType.CyanPaint:
                case ItemType.PinkPaint:
                case ItemType.OrangePaint:
                case ItemType.PurplePaint:
                    returnItem = HoldItem(item);
                    _paintHeld = true;
                    _paintType = item.type;
                    SetPaintStationColor();
                    break;
                case ItemType.UnpaintedTrainParts:
                    _trainPartsHeld = true;
                    item.DeleteItem();
                    break;
            }

            if (_paintHeld && _trainPartsHeld)
            {
                timer.StartTimer(5);
            }
            return returnItem;

        }

        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.RedPaint:
                case ItemType.GreenPaint:
                case ItemType.BluePaint:
                case ItemType.YellowPaint:
                case ItemType.CyanPaint:
                case ItemType.PinkPaint:
                case ItemType.OrangePaint:
                case ItemType.PurplePaint:
                case ItemType.UnpaintedTrainParts:
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
            }
        }

        private void Transform()
        {
            DestroyAllItems();

            var type = GetTrainPartType();
            var item = itemManager.CreateItem(type, transform);
            HoldItem(item);

            _paintType = default;
            _paintHeld = false;
            _trainPartsHeld = false;
            SetPaintStationColor();
        }

        private ItemType GetTrainPartType()
        {
            ItemType colorTrainPartsType;
            switch (_paintType)
            {
                case ItemType.RedPaint:
                    colorTrainPartsType = ItemType.RedTrainParts;
                    break;
                case ItemType.GreenPaint:
                    colorTrainPartsType = ItemType.GreenTrainParts;
                    break;
                case ItemType.BluePaint:
                    colorTrainPartsType = ItemType.BlueTrainParts;
                    break;
                case ItemType.YellowPaint:
                    colorTrainPartsType = ItemType.YellowTrainParts;
                    break;
                case ItemType.CyanPaint:
                    colorTrainPartsType = ItemType.CyanTrainParts;
                    break;
                case ItemType.PinkPaint:
                    colorTrainPartsType = ItemType.PinkTrainParts;
                    break;
                case ItemType.OrangePaint:
                    colorTrainPartsType = ItemType.OrangeTrainParts;
                    break;
                case ItemType.PurplePaint:
                    colorTrainPartsType = ItemType.PurpleTrainParts;
                    break;
                default:
                    colorTrainPartsType = ItemType.UnpaintedTrainParts;
                    break;
            }
            return colorTrainPartsType;
        }

        private void SetPaintStationColor()
        {
            switch (_paintType)
            {
                case ItemType.RedPaint:
                    _spriteRenderer.sprite = paintStationColorSprites[0];
                    break;
                case ItemType.GreenPaint:
                    _spriteRenderer.sprite = paintStationColorSprites[1];
                    break;
                case ItemType.BluePaint:
                    _spriteRenderer.sprite = paintStationColorSprites[2];
                    break;
                case ItemType.YellowPaint:
                    _spriteRenderer.sprite = paintStationColorSprites[3];
                    break;
                case ItemType.CyanPaint:
                    _spriteRenderer.sprite = paintStationColorSprites[4];
                    break;
                case ItemType.PinkPaint:
                    _spriteRenderer.sprite = paintStationColorSprites[5];
                    break;
                case ItemType.OrangePaint:
                    _spriteRenderer.sprite = paintStationColorSprites[6];
                    break;
                case ItemType.PurplePaint:
                    _spriteRenderer.sprite = paintStationColorSprites[7];
                    break;
                default:
                    _spriteRenderer.sprite = paintStationColorSprites[8];
                    break;
            }
        }
    }
}
