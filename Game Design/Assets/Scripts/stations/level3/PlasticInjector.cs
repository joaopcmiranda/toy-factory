using items;
using items.handling;
using managers;

namespace stations
{
    public class PlasticInjector : ItemHolder
    {

        public Timer timer;

        private LevelManager _levelManager;

        public override bool CanReceiveItem(Item item)
        {
            return item.type == ItemType.Plastic;
        }

        public override void Start()
        {
            base.Start();
            _levelManager = FindObjectOfType<LevelManager>();
        }

        public override Item GetItem()
        {
            timer.ResetTimer();
            return ReleaseLastItem();
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;

            timer.StartTimer(5);
            return HoldItem(item);
        }

        private void Update()
        {
            if (timer.IsTimeUp() && timer.IsActive())
            {
                Transform();
                timer.ResetTimer();
            }
        }

        private void Transform()
        {
            ReleaseLastItem()
                ?.DeleteItem();

            ItemType outputType;
            //you will need to start from the Game scene
            switch (_levelManager.GetLevelScene())
            {
                case 1:
                    outputType = ItemType.UnpaintedTrainParts;
                    break;
                default:
                    outputType = ItemType.PaintedTrainParts;
                    break;
            }
            var parts = itemManager.CreateItem(outputType, transform);
            HoldItem(parts);
        }
    }
}
