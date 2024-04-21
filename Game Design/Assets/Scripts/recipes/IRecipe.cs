using items;
using UnityEngine;
namespace recipes
{
    public interface IRecipe
    {
        public string recipeName { get; }
        public int frequency { get; }
        public int timeLimit { get; }
        public int points { get; }
        public ItemType deliveryItem { get; }

        public Sprite GetIcon();
    }
}
