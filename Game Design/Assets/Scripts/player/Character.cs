using System;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace player
{
    public enum CharacterControls
    {
        Keyboard1,
        Keyboard2,
    }

    public class Character : MonoBehaviour
    {
        public float speed = 5.0f;
        public CharacterControls controls = CharacterControls.Keyboard1;

        private SelectionRayCaster _rayCaster;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _lastDirection = Vector2.right;
        private Animator _animator;
        private bool _isWalking;

        private static readonly int AnimX = Animator.StringToHash("X");
        private static readonly int AnimY = Animator.StringToHash("Y");
        private static readonly int AnimIsWalking = Animator.StringToHash("isWalking");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _rayCaster = GetComponent<SelectionRayCaster>();
        }

        // Update is called once per frame
        private void Update()
        {
            _movement.x = Input.GetAxisRaw(controls == CharacterControls.Keyboard1 ? "Horizontal" : "Horizontal2");
            _movement.y = Input.GetAxisRaw(controls == CharacterControls.Keyboard1 ? "Vertical" : "Vertical2");

            if (_movement != Vector2.zero)
            {
                _isWalking = true;
                _animator.SetFloat(AnimX, _movement.x);
                _animator.SetFloat(AnimY, _movement.y);

                _animator.SetBool(AnimIsWalking, true);

                _lastDirection = _movement.normalized; // Update lastDirection when the player moves

                _rb.MovePosition(_rb.position + _movement.normalized * (speed * Time.deltaTime));

                _rayCaster.CastTorwards(_movement);
            }
            else if (_isWalking)
            {
                StopWalking();
            }

        }

        private void StopWalking()
        {
            _isWalking = false;
            _animator.SetBool(AnimIsWalking, false);
            _animator.SetFloat(AnimX, _lastDirection.x);
            _animator.SetFloat(AnimY, _lastDirection.y);
            _movement = Vector2.zero;
        }

        public Vector2 GetFacingDirection()
        {
            return _lastDirection;
        }
    }
}
