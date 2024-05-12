using UnityEngine;
using items;

namespace machines
{

    public class MaterialStation_Level4 : Machine_Level4
    {

        public Item_Level4 rawMaterialType;

        public override void Start()
        {
            GenerateNewMaterial();
            base.Start(); 
        }

        public override Item_Level4 TakeItemFromMachine()
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

                var newItem = newMaterialObj.GetComponent<Item_Level4>();

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
