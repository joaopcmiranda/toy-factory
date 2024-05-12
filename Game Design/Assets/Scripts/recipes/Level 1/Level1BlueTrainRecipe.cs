using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class Level1BlueTrainRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Plastic Blue Train";
        public int frequency { get; set; } = 20;
        public int timeLimit { get; set; } = 105;
        public int points { get; set; } = 150;
        public Sprite trainIcon;
        public ItemType deliveryItem
        {
            get
            {
                return ItemType.BlueTrain;
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
