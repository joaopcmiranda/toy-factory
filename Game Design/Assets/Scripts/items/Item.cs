using items.handling;
using UnityEngine;
using managers;

namespace items
{
    public class Item : MonoBehaviour
    {
        public ItemType type;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private static ItemManager _itemManager;
        private IItemHandler _heldBy;

        private void Awake()
        {
            if (_itemManager == null)
            {
                _itemManager = FindObjectOfType<ItemManager>();
            }
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetItemColor(Color color)
        {
            if (_spriteRenderer)
            {
                _spriteRenderer.color = color;
            }
        }

        public void PickUp(IItemHandler holder, Transform holdSpot)
        {
            if (_rigidbody2D)
            {
                _rigidbody2D.simulated = false;
            }

            transform.position = holdSpot.position;
            transform.SetParent(holdSpot);
            _heldBy = holder;
            _itemManager?.RefreshItems();
        }

        public void Drop()
        {
            if (_rigidbody2D)
            {
                _rigidbody2D.simulated = true;
            }

            transform.SetParent(null);
            _heldBy = null;
            _itemManager?.RefreshItems();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void DeleteItem()
        {
            Destroy(gameObject);
            _itemManager?.RefreshItems();
        }
    }
}
