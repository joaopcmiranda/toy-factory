using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class RedTrain : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Red Train";
        public int frequency { get; set; } = 1;
        public int timeLimit { get; set; } = 60;
        public int points { get; set; } = 100;
        public Sprite trainIcon;
        public ItemType deliveryItem
        {
            get => ItemType.RedTrain;
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