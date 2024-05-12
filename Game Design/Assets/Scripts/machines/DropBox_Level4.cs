using UnityEngine;
using items;
using score;

namespace machines
{ 
    public class DropBox_Level4 : Machine_Base_Level4
    {
        public ScoreManager scoreManager;

        public override void Start()
        {
            base.Start();
        }
        public override void HoldItem(Item_Level4 item)
        {

            //if item == train complete the level
            if (item.type == ItemType.PaintedTrain)
            {
                scoreManager.IncreaseScore(100); 
            }
            item.DeleteItem();
        }
    }

}
