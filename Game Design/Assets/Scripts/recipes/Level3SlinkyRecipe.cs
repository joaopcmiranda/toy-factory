using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class Level3SlinkyRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Metal Slinky";
        public int frequency { get; set; } = 1;
        public int timeLimit { get; set; } = 60;
        public int points { get; set; } = 100;
        public Sprite slinkyIcon;
        public ItemType deliveryItem
        {
            get => ItemType.Slinky;
        }

        public Sprite GetIcon()
        {
            return slinkyIcon;
        }

        private void Awake()
        {
            gameObject.GetComponent<OrderManager>().RegisterRecipe(this);
        }
    }
}
