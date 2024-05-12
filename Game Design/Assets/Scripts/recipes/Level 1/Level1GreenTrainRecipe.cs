using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class Level1GreenTrainRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Plastic Green Train";
        public int frequency { get; set; } = 20;
        public int timeLimit { get; set; } = 60;
        public int points { get; set; } = 100;
        public Sprite trainIcon;
        public ItemType deliveryItem
        {
            get
            {
                return ItemType.GreenTrain;
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
