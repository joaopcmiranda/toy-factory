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
            orderManager = FindObjectOfType<OrderManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        public override void HoldItem(Item item)
        {

            var success = orderManager.FinishOrder(item);
            if (success)
            {
                item.DeleteItem();
                Debug.Log("Delivered");
            }
            else
            {
                scoreManager.DecreaseScore(100);
            }
        }

        private void Update()
        {
            
        }

    }

}
