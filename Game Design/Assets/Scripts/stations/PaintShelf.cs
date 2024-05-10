using System;
using System.ComponentModel;
using items;
using items.handling;

namespace stations
{
    public class PaintShelf : ItemProvider
    {
        public override ItemType itemType
        {
            get => ItemType.Paint;
            set
            {
                if (!Enum.IsDefined(typeof(ItemType), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ItemType));
                itemType = value;
            }
        }
    }
}
