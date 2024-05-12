using UnityEngine;

namespace player
{
    public class Character_Level4 : MonoBehaviour
    {

        public bool active = true;

        public float speed = 5.0f;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _lastDirection = Vector2.right;
        private Animator _animator;
        private bool _isWalking = false;

        private static readonly int AnimX = Animator.StringToHash("X");
        private static readonly int AnimY = Animator.StringToHash("Y");
        private static readonly int AnimIsWalking = Animator.StringToHash("isWalking");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }


        // Update is called once per frame
        private void Update()
        {
            if (active)
            {
                _movement.x = Input.GetAxisRaw("Horizontal");
                _movement.y = Input.GetAxisRaw("Vertical");

                if (_movement != Vector2.zero)
                {
                    _isWalking = true;
                    _animator.SetFloat(AnimX, _movement.x);
                    _animator.SetFloat(AnimY, _movement.y);

                    _animator.SetBool(AnimIsWalking, true);

                    _lastDirection = _movement.normalized; // Update lastDirection when the player moves
                }
                else if (_isWalking)
                {
                    _isWalking = false;
                    _animator.SetBool(AnimIsWalking, false);
                    _animator.SetFloat(AnimX, 0);
                    _animator.SetFloat(AnimY, 0);
                    _lastDirection = _movement.normalized;
                    _movement = Vector2.zero;
                }
            }
            else if (_isWalking)
            {
                _isWalking = false;
                _animator.SetBool(AnimIsWalking, false);
                _animator.SetFloat(AnimX, 0);
                _animator.SetFloat(AnimY, 0);
                _lastDirection = _movement.normalized;
                _movement = Vector2.zero;
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
