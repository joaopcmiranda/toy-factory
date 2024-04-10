using UnityEngine;
using items;

namespace machines
{

    public class MaterialStation : Machine
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

            GenerateNewMaterial();
            return item;
        }

        public override void HoldItem(Item item)
        {
            base.HoldItem(item);
        }

        private void GenerateNewMaterial()
        {
            if (rawMaterialType != null)
            {
                GameObject newMaterialObj = Instantiate(rawMaterialType.gameObject, holdSpot.position, Quaternion.identity);

                Item newItem = newMaterialObj.GetComponent<Item>();

                Rigidbody2D itemRb = newMaterialObj.GetComponent<Rigidbody2D>();
                if (itemRb != null)
                {
                    itemRb.simulated = true;
                }
                HoldItem(newItem);
            }
        }
    }
}
