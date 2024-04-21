using managers;
using UnityEngine;
namespace recipes
{
    public class Level0TrainRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Plastic Train";
        public int frequency { get; set; } = 1;
        public int timeLimit { get; set; } = 60;
        public int points { get; set; } = 100;
        public Sprite trainIcon;

        public Sprite GetIcon()
        {
            return trainIcon;
        }

        private void Start()
        {
            gameObject.GetComponent<OrderManager>().RegisterRecipe(this);
        }
    }
}
