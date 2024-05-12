using items;
using items.handling;
using score;

namespace stations
{
    public class DropBox : ItemReceiver
    {
        private Order[] _orders;
        private OrderManager _orderManager;
        private ScoreManager _scoreManager;

        public override void Start()
        {
            base.Start();
            _orderManager = FindObjectOfType<OrderManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        protected override Item HandleItem(Item item)
        {
            var success = _orderManager.FinishOrder(item);

            if (!success)
            {
                _scoreManager.DecreaseScore(100);
            }

            item.DeleteItem();

            return null;
        }

        public override bool CanReceiveItem(Item item)
        {
            return true;
        }
    }
}
