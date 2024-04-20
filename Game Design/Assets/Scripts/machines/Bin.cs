using items;
using UnityEngine;

namespace machines
{
    public class Bin : Machine
    {
        public override void HoldItem(Item item)
        {
            item.DeleteItem();
        }

    }
}
