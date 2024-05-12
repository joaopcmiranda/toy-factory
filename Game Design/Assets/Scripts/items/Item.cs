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
        public bool IsHeldByMachine { get; set; }

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

        private void Update()
        {
            
        }

        public void SetItemColor(Color color)
        {
            if (_spriteRenderer)
            {
                _spriteRenderer.color = color;
            }
        }

        public void PickUp(Transform holdSpot)
        {
            if (_rigidbody2D)
            {
                _rigidbody2D.simulated = false;
            }

            transform.position = holdSpot.position;
            transform.SetParent(holdSpot);
            _itemManager?.RefreshItems();
        }

        public void Drop()
        {
            if (_rigidbody2D)
            {
                _rigidbody2D.simulated = true;
            }

            transform.SetParent(null);
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

        public void setSpriteSize(Vector3 size)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.transform.localScale = size; 
        }
    }
}
