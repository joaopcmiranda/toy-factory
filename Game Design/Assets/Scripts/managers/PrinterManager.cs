using TMPro;
using UnityEngine;

public class PrinterManager : MonoBehaviour, IMachineManager
namespace managers
{
    public class PrinterManager : MonoBehaviour
    {

    public TextMeshProUGUI uiText;
    public Transform holdSpot;
    public LayerMask pickUpMask;
    [SerializeField] private float _dropRadius = 1f;

    public float dropRadius => _dropRadius;
    public Transform MachineTransform => transform;

    private GameObject itemHolding;
    public Sprite trainSprite;

        public void HoldItem(GameObject item)
        {
            if (_itemHolding) return; // If not already holding an item

            _itemHolding = item;
            _itemHolding.transform.position = holdSpot.position; // Move to printer's hold spot
            _itemHolding.transform.SetParent(transform); // Parent to the printer

            // Disable physics so that the item doesn't fall or is affected by other forces
            var itemRb = _itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb)
            {
                itemRb.simulated = false;
            }

            uiText.text = "3D Printing..."; // Update UI to show printing status
        }

        public GameObject TakeItem()
        {
            if (!_itemHolding) return null;
            // Retrieve the item from the printer
            var item = _itemHolding;

            // Optionally, if you disabled physics when putting the item in the printer, re-enable it here
            var itemRb = _itemHolding.GetComponent<Rigidbody2D>();
            if (itemRb)
            {
                itemRb.simulated = true;
            }

            item.transform.SetParent(null);

            TransformPlastic(item);

            _itemHolding = null;

            return item;
        }

        private void TransformPlastic(GameObject itemHolding)
        {
            if (!itemHolding.CompareTag("Plastic")) return;

            uiText.text = "3D Print Plastic done";
            var plasticSpriteRenderer = itemHolding.GetComponent<SpriteRenderer>();
            plasticSpriteRenderer.sprite = trainSprite;
            itemHolding.tag = "Train";
        }

        public bool IsHoldingItem()
        {
            return _itemHolding;
        }

    }
}
