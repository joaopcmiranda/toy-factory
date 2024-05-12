using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class Level3GreenTrainRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Plastic Train";
        public int frequency { get; set; } = 4;
        public int timeLimit { get; set; } = 90;
        public int points { get; set; } = 200;
        public Sprite trainIcon;
        public ItemType deliveryItem
        {
            get => ItemType.GreenTrain;
        }

        public Sprite GetIcon()
        {
            return trainIcon;
        }

        private void Awake()
        {
            gameObject.GetComponent<OrderManager>().RegisterRecipe(this);
        }
    }
}
