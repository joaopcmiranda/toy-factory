using UnityEngine;
using items;
using score;

namespace machines
{
    public class DropBox : Machine
    {
        private Order[] orders;
        private OrderManager orderManager;
        private ScoreManager scoreManager;

        public override void Start()
        {
            base.Start();
        }
        public override void HoldItem(Item item)
        {

            //if item == train complete the level
            if (item)
            {
                Debug.Log("Delivered");
                Debug.Log("Ending Level!");
            }
            else
            {
                scoreManager.DecreaseScore(100);
            }
            item.DeleteItem();
        }
    }

}
