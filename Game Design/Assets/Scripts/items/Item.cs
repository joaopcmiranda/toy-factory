using UnityEngine;
using managers;

namespace items
{
    public class Item : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private static ItemManager _itemManager;
        public bool IsHeldByMachine { get; set; }

        private void Awake()
        {
            if (_itemManager == null)
            {
                _itemManager = FindObjectOfType<ItemManager>();
                if (_itemManager == null)
                    Debug.LogError("ItemManager not found in the scene!");
                else
                    Debug.Log("ItemManager found and set.");
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
    }
}
