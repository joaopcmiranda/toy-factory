using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class Level1RedTrainRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Plastic Red Train";
        public int frequency { get; set; } = 20;
        public int timeLimit { get; set; } = 75;
        public int points { get; set; } = 100;
        public Sprite trainIcon;
        public ItemType deliveryItem
        {
            get
            {
                return ItemType.RedTrain;
            }
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
