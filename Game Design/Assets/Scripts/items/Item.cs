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
        private Collider2D[] _colliders;
        private static ItemManager _itemManager;
        private IItemHandler _heldBy;

        private void Awake()
        {
            if (_itemManager == null)
            {
                _itemManager = FindObjectOfType<ItemManager>();
            }
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _colliders = GetComponents<Collider2D>();
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
            if (_colliders != null)
            {
                foreach (var col in _colliders)
                {
                    col.enabled = false;
                }
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
            if (_colliders != null)
            {
                foreach (var col in _colliders)
                {
                    col.enabled = true;
                }
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
        
        public SpriteRenderer getSpriteRenderer()
        {
            return GetComponent<SpriteRenderer>();
        }

        public Sprite getSprite()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer.sprite; 
        }

        public Vector3 getSpriteSize()
        {
            return GetComponent<SpriteRenderer>().bounds.size;
        }

        public void setItemSize(Vector3 size)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer.transform.localScale = size; 
            boxCollider.size = new Vector2(.5f ,.5f);
        }
    }
}
