using UnityEngine;

namespace player
{
    public class PlayerMovement : MonoBehaviour
    {

        public float speed = 5.0f;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _lastDirection = Vector2.right;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            if (_movement != Vector2.zero)
            {
                _lastDirection = _movement.normalized; // Update lastDirection when the player moves
            }

        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * (speed * Time.fixedDeltaTime));
        }

        public Vector2 GetFacingDirection()
        {
            return _lastDirection;
        }
    }
}
