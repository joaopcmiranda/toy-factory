using items;
using managers;
using UnityEngine;
namespace recipes
{
    public class Level3CubeRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Puzzle Cube";
        public int frequency { get; set; } = 10;
        public int timeLimit { get; set; } = 60;
        public int points { get; set; } = 150;
        public Sprite cubeIcon;
        public ItemType deliveryItem
        {
            get => ItemType.PuzzleCube;
        }

        public Sprite GetIcon()
        {
            return cubeIcon;
        }

        private void Awake()
        {
            gameObject.GetComponent<OrderManager>().RegisterRecipe(this);
        }
    }
}
