using UnityEngine;
using items;
using score;

namespace machines
{ 
    public class DropBox : Machine
    {
        public ScoreManager scoreManager;

        public override void Start()
        {
            base.Start();
        }
        public override void HoldItem(Item item)
        {

            //if item == train complete the level
            if (item)
            {
                scoreManager.IncreaseScore(100); 
            }
            item.DeleteItem();
        }
    }

}
