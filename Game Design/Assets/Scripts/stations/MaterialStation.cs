using UnityEngine;
using items;
using items.handling;

namespace stations
{

    public class MaterialStation : ItemProvider
    {

        public ItemType providedType;
        public override ItemType itemType
        {
            get => providedType;
        }

    }
}
