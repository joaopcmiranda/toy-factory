using UnityEngine;
using items;
using score;

namespace machines
{
    public class DropBox : Machine
    {
        private Order[] orders;
        private ScoreManager scoreManager;
        private OrderManager orderManager;
        public override void Start()
        {
            base.Start();
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        public override void HoldItem(Item item)
        {
            for(int i = 0; i < orderManager.getOrders().Length; i++)
            {
                if (item.CompareTag("Train"))
                {
                    scoreManager.IncreaseScore(300);
                    orderManager.ReplaceOrder(i);
                }
                else
                {
                    scoreManager.DecreaseScore(100);
                }
                item.DeleteItem();
            }

            foreach(Order order in orderManager.getOrders())
            {

            }
        }

    }

}
