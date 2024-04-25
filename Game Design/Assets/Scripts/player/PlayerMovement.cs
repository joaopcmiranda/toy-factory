using UnityEngine;

namespace player
{
    public class PlayerMovement : MonoBehaviour
    {

        public float speed = 5.0f;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _lastDirection = Vector2.right;
        private Animator animator;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }


        // Update is called once per frame
        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            if (_movement != Vector2.zero)
            {
                animator.SetFloat("X", _movement.x);
                animator.SetFloat("Y", _movement.y);

                animator.SetBool("isWalking", true); 

                _lastDirection = _movement.normalized; // Update lastDirection when the player moves
            } else
            {
                animator.SetBool("isWalking", false);
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
