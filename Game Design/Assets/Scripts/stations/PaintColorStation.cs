using System.Collections.Generic;
using managers;
using UnityEngine;
using items.handling;
using items;

namespace stations
{
    public class PaintColorStation : ItemHolder
    {
        public List<Sprite> paintStationColorSprites;
        public Timer timer;

        private ItemType _paintType;
        private bool _paintHeld;
        private bool _trainPartsHeld;
        private ItemType _lastReceivedType;

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
                    item.gameObject.SetActive(false);
                    _paintHeld = true;
                    _paintType = item.type;
                    SetPaintStationColor();
                    Destroy(item.gameObject);
                    break;
                case ItemType.UnpaintedTrainParts:
                case ItemType.UnpaintedCarriageParts:
                    _trainPartsHeld = true;
                    _lastReceivedType = item.type;
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
                case ItemType.UnpaintedCarriageParts:
                    return true;
                default:
                    return false;
            }
        }

        private void Update()
        {
            if (timer.IsTimeUp() && timer.IsActive())
            {
                TransformItems();
                timer.ResetTimer();
            }
        }

        private void TransformItems()
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
            bool isCarriage = _trainPartsHeld && _lastReceivedType == ItemType.UnpaintedCarriageParts;
            switch (_paintType)
            {
                case ItemType.RedPaint:
                    return isCarriage ? ItemType.RedCarriageParts : ItemType.RedTrainParts;
                case ItemType.GreenPaint:
                    return isCarriage ? ItemType.GreenCarriageParts : ItemType.GreenTrainParts;
                case ItemType.BluePaint:
                    return isCarriage ? ItemType.BlueCarriageParts : ItemType.BlueTrainParts;
                case ItemType.YellowPaint:
                    return isCarriage ? ItemType.YellowCarriageParts : ItemType.YellowTrainParts;
                case ItemType.CyanPaint:
                    return isCarriage ? ItemType.CyanCarriageParts : ItemType.CyanTrainParts;
                case ItemType.PinkPaint:
                    return isCarriage ? ItemType.PinkCarriageParts : ItemType.PinkTrainParts;
                case ItemType.OrangePaint:
                    return isCarriage ? ItemType.OrangeCarriageParts : ItemType.OrangeTrainParts;
                case ItemType.PurplePaint:
                    return isCarriage ? ItemType.PurpleCarriageParts : ItemType.PurpleTrainParts;
                default:
                    return isCarriage ? ItemType.UnpaintedCarriageParts : ItemType.UnpaintedTrainParts;
            }
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

