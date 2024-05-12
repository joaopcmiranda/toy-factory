using items;
using items.handling;

namespace stations
{
    public class PaintShelf : ItemProvider
    {
        public override ItemType itemType
        {
            get => ItemType.Paint;
        }
    }
}
