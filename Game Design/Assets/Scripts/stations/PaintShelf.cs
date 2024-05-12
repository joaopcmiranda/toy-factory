using items;
using items.handling;

namespace stations
{
    public class PaintShelf : ItemProvider
    {
        public ItemType providedType = ItemType.Paint;
        public override ItemType itemType
        {
            get => providedType;
        }
    }
}
