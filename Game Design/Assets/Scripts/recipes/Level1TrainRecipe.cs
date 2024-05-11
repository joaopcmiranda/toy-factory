using items;
using managers;
using System.Collections.Generic;
using UnityEngine;
namespace recipes
{
    public class Level1TrainRecipe : MonoBehaviour, IRecipe
    {
        public string recipeName { get; set; } = "Plastic Train";
        public int frequency { get; set; } = 1;
        public int timeLimit { get; set; } = 60;
        public int points { get; set; } = 100;

        public bool recreateRecipe { get; set; } = false;

        public List<Sprite> trainIcons;
        private int trainIndex = 0;

        public ItemType deliveryItem { get; set; } = ItemType.Train;

        public Sprite GetIcon()
        {
            return trainIcons[trainIndex];
        }

        private void Awake()
        {
            trainIndex = Random.Range(0, trainIcons.Count);
            gameObject.GetComponent<OrderManager>().RegisterRecipe(this);
        }

        private void Start()
        {
            createRecipe();
        }

        void Update()
        {
            if (recreateRecipe)
            {
                createRecipe();
            }
        }

        void createRecipe()
        {
            Debug.LogWarning("trainIndex: " + trainIndex);
            switch (trainIndex)
            {
                case 0:
                    recipeName = "Plastic Red Train";
                    deliveryItem = ItemType.RedTrain;
                    break;
                case 1:
                    recipeName = "Plastic Green Train";
                    deliveryItem = ItemType.GreenTrain;
                    break;
                case 2:
                    recipeName = "Plastic Blue Train";
                    deliveryItem = ItemType.BlueTrain;
                    break;
                case 3:
                    recipeName = "Plastic Yellow Train";
                    deliveryItem = ItemType.YellowTrain;
                    break;
                case 4:
                    recipeName = "Plastic Cyan Train";
                    deliveryItem = ItemType.CyanTrain;
                    break;
                case 5:
                    recipeName = "Plastic Pink Train";
                    deliveryItem = ItemType.PinkTrain;
                    break;
                case 6:
                    recipeName = "Plastic Orange Train";
                    deliveryItem = ItemType.OrangeTrain;
                    break;
                case 7:
                    recipeName = "Plastic Purple Train";
                    deliveryItem = ItemType.PurpleTrain;
                    break;
                default:
                    recipeName = "Plastic Train";
                    deliveryItem = ItemType.Train;
                    break;
            }
            Debug.Log(recipeName);
            Debug.Log(deliveryItem);
            trainIndex = Random.Range(0, trainIcons.Count);

            recreateRecipe = false;
        }
    }
}
