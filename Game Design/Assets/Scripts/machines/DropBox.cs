using UnityEngine;
using items;

namespace machines
{
    public class DropBox : Machine
    {
        private Order[] orders;
        private OrderManager orderManager;
        public override void Start()
        {
            orderManager = FindObjectOfType<OrderManager>();
        }
        public override void HoldItem(Item item)
        {
            for(int i = 0; i < orderManager.getOrders().Length; i++)
            {
                if (item.CompareTag("Train"))
                {
                    orderManager.increaseScore(300);
                    orderManager.ReplaceOrder(i);
                }
                else
                {
                    orderManager.decreaseScore(100);
                }
                item.DeleteItem();
            }

            foreach(Order order in orderManager.getOrders())
            {
                
            }
        }

        //SELLITEM
    }

}
