using UnityEngine;
using items;
using items.handling;
using score;

namespace machines
{
    public class DropBox : ItemHolder
    {
        public ScoreManager scoreManager;

        public override Item GetItem()
        {
            throw new System.NotImplementedException();
        }
        public override Item PutItem(Item item)
        {
            //if item == train complete the level
            if (item.type == ItemType.PaintedTrain)
            {
                scoreManager.IncreaseScore(100);
            }
            item.DeleteItem();
            return null;
        }
        public override bool CanReceiveItem(Item item)
        {
            throw new System.NotImplementedException();
        }
        public override void Start()
        {
            base.Start();
        }
    }

}
