using UnityEngine;
using items;
using score;

namespace machines
{
    public class DropBox : Machine
    {
        public override void Start()
        {
            base.Start();
        }
        public override void HoldItem(Item item)
        {

            //if item == train complete the level
            if (item)
            {
                Debug.Log("He Loves it...");
                Debug.Log("Cause he loves you");
            }
            item.DeleteItem();
        }
    }

}
