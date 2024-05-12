using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class RedCarriage : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Red Carriage";
        public int frequency { get; set; } = 1;
        public int timeLimit { get; set; } = 120;
        public int points { get; set; } = 200;
        public Sprite trainIcon;
        public ItemType deliveryItem
        {
            get => ItemType.RedCarriage;
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