using items;
using items.handling;

namespace stations
{
    public class AssemblyLevel3 : ItemHolder
    {
        private ItemType _currentRecipe = ItemType.None;

        // Train
        private bool _isHoldingTrainParts;
        private bool _isHoldingWheels;
        private bool _isHoldingTrainBody;

        // Cube
        private bool _isHoldingRedCubeParts;
        private bool _isHoldingGreenCubeParts;
        private bool _isHoldingBlueCubeParts;

        private Timer _timer;

        private void Awake()
        {
            _timer = GetComponent<Timer>();
        }

        public override bool CanReceiveItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.Wheels:
                    return !_isHoldingWheels && _currentRecipe != ItemType.PuzzleCube;
                case ItemType.MetalTrainBody:
                    return !_isHoldingTrainBody && _currentRecipe != ItemType.PuzzleCube;
                case ItemType.RedTrainParts:
                    return !_isHoldingTrainParts && (_currentRecipe == ItemType.RedTrain || _currentRecipe == ItemType.Train || _currentRecipe == ItemType.None);
                case ItemType.GreenTrainParts:
                    return !_isHoldingTrainParts && (_currentRecipe == ItemType.GreenTrain || _currentRecipe == ItemType.Train || _currentRecipe == ItemType.None);
                case ItemType.BlueTrainParts:
                    return !_isHoldingTrainParts && (_currentRecipe == ItemType.BlueTrain || _currentRecipe == ItemType.Train || _currentRecipe == ItemType.None);
                case ItemType.RedCubeParts:
                    return !_isHoldingRedCubeParts && _currentRecipe == ItemType.PuzzleCube || _currentRecipe == ItemType.None;
                case ItemType.GreenCubeParts:
                    return !_isHoldingGreenCubeParts && _currentRecipe == ItemType.PuzzleCube || _currentRecipe == ItemType.None;
                case ItemType.BlueCubeParts:
                    return !_isHoldingBlueCubeParts && _currentRecipe == ItemType.PuzzleCube || _currentRecipe == ItemType.None;
                default:
                    return false;
            }
        }

        public override Item PutItem(Item item)
        {
            if (!CanReceiveItem(item)) return item;

            switch (item.type)
            {
                case ItemType.Wheels:
                    _isHoldingWheels = true;
                    if (!IsTrainType()) _currentRecipe = ItemType.Train;
                    break;
                case ItemType.MetalTrainBody:
                    _isHoldingTrainBody = true;
                    if (!IsTrainType()) _currentRecipe = ItemType.Train;
                    break;
                case ItemType.RedTrainParts:
                    _currentRecipe = ItemType.RedTrain;
                    _isHoldingTrainParts = true;
                    break;
                case ItemType.GreenTrainParts:
                    _currentRecipe = ItemType.GreenTrain;
                    _isHoldingTrainParts = true;
                    break;
                case ItemType.BlueTrainParts:
                    _currentRecipe = ItemType.BlueTrain;
                    _isHoldingTrainParts = true;
                    break;
                case ItemType.RedCubeParts:
                    _isHoldingRedCubeParts = true;
                    if (_currentRecipe == ItemType.None) _currentRecipe = ItemType.PuzzleCube;
                    break;
                case ItemType.GreenCubeParts:
                    _isHoldingGreenCubeParts = true;
                    if (_currentRecipe == ItemType.None) _currentRecipe = ItemType.PuzzleCube;
                    break;
                case ItemType.BlueCubeParts:
                    _isHoldingBlueCubeParts = true;
                    if (_currentRecipe == ItemType.None) _currentRecipe = ItemType.PuzzleCube;
                    break;
            }

            if (IsReadyToTransform())
            {
                _timer.StartTimer(5);
            }

            return HoldItem(item);
        }

        private bool IsTrainType()
        {
            return _currentRecipe == ItemType.Train || _currentRecipe == ItemType.RedTrain || _currentRecipe == ItemType.GreenTrain || _currentRecipe == ItemType.BlueTrain;
        }

        private bool IsReadyToTransform()
        {
            return (_isHoldingWheels && _isHoldingTrainBody && _isHoldingTrainParts)
                || (_isHoldingRedCubeParts && _isHoldingGreenCubeParts && _isHoldingBlueCubeParts);
        }

        public override Item GetItem()
        {
            var item = ReleaseLastItem();

            switch (item.type)
            {
                case ItemType.Wheels:
                    _isHoldingWheels = false;
                    break;
                case ItemType.MetalTrainBody:
                    _isHoldingTrainBody = false;
                    break;
                case ItemType.RedTrainParts:
                case ItemType.GreenTrainParts:
                case ItemType.BlueTrainParts:
                    _isHoldingTrainParts = false;
                    break;
                case ItemType.RedCubeParts:
                    _isHoldingRedCubeParts = false;
                    break;
                case ItemType.GreenCubeParts:
                    _isHoldingGreenCubeParts = false;
                    break;
                case ItemType.BlueCubeParts:
                    _isHoldingBlueCubeParts = false;
                    break;
            }

            if (!_isHoldingWheels && !_isHoldingTrainBody && !_isHoldingTrainParts && !_isHoldingRedCubeParts && !_isHoldingGreenCubeParts && !_isHoldingBlueCubeParts)
            {
                _currentRecipe = ItemType.None;
            }

            return item;
        }

        private void Update()
        {
            if (_timer.IsActive() && _timer.IsTimeUp())
            {
                Transform();
                _timer.ResetTimer();
            }
        }


        private void Transform()
        {
            var newItem = itemManager.CreateItem(_currentRecipe, transform);

            DestroyAllItems();

            _isHoldingWheels = false;
            _isHoldingTrainBody = false;
            _isHoldingTrainParts = false;
            _isHoldingRedCubeParts = false;
            _isHoldingGreenCubeParts = false;
            _isHoldingBlueCubeParts = false;

            HoldItem(newItem);
        }
    }
}
