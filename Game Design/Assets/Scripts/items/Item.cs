using UnityEngine;

namespace items
{
    public class Item : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void PickUp(Transform holdSpot)
        {
            if (_rigidbody2D)
            {
                _rigidbody2D.simulated = false;
            }

            transform.position = holdSpot.position;
            transform.SetParent(holdSpot);
        }

        public void Drop()
        {
            if (_rigidbody2D)
            {
                _rigidbody2D.simulated = true;
            }

            transform.SetParent(null);
        }
    }
}
