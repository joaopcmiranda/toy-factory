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

        private void GenerateNewMaterial()
        {
            if (rawMaterialType)
            {
                var newMaterialObj = Instantiate(rawMaterialType.gameObject, holdSpot.position, Quaternion.identity);

                var newItem = newMaterialObj.GetComponent<Item>();

                var itemRb = newMaterialObj.GetComponent<Rigidbody2D>();
                if (itemRb)
                {
                    itemRb.simulated = false;
                }
                HoldItem(newItem);
            }
        }
    }
}
