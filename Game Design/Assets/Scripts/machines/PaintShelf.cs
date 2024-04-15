using UnityEngine;
using items;

namespace machines
{

    public class PaintShelf : Machine
    {

        public Item rawMaterialType;

        public override void Start()
        {
            GenerateNewMaterial();
            base.Start();
        }

        public override Item TakeItemFromMachine()
        {
            var item = base.TakeItemFromMachine();
            item.GetComponent<SpriteRenderer>().enabled = true;

            GenerateNewMaterial();
            return item;
        }

        private void GenerateNewMaterial()
        {
            if (rawMaterialType)
            {
                var newMaterialObj = Instantiate(rawMaterialType.gameObject, holdSpot.position, Quaternion.identity);

                var newItem = newMaterialObj.GetComponent<Item>();

                newMaterialObj.GetComponent<SpriteRenderer>().enabled = false;

                var itemRb = newMaterialObj.GetComponent<Rigidbody2D>();
                if (itemRb)
                {
                    itemRb.simulated = true;
                }
                HoldItem(newItem);
            }
        }
    }
}
