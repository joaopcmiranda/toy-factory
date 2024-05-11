using System;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace player
{
    public class Character : MonoBehaviour
    {

        public bool active = true;
        public float speed = 5.0f;
        public float interactionRadius = 1.5f;
        public Transform rayCaster;

        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _lastDirection = Vector2.right;
        private Animator _animator;
        private bool _isWalking;
        private GameObject _selectedObject;

        private static readonly int AnimX = Animator.StringToHash("X");
        private static readonly int AnimY = Animator.StringToHash("Y");
        private static readonly int AnimIsWalking = Animator.StringToHash("isWalking");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private ContactFilter2D _contactFilter = new ContactFilter2D();
        private void Start()
        {
            _contactFilter.layerMask = Physics2D.AllLayers;
            _contactFilter.useLayerMask = true;
            _contactFilter.useTriggers = true;

            // Stop the ray from colliding with the current gameobject's collider
            _contactFilter.SetLayerMask(~((1 << gameObject.layer) | (1 << 2)));
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

                    _rb.MovePosition(_rb.position + _movement.normalized * (speed * Time.deltaTime));

                    // cast a selecting ray in the facing direction
                    // if the ray hits a machine, highlight it
                    Vector2 origin = rayCaster.position;
                    Debug.DrawRay(origin, _lastDirection * interactionRadius, Color.red, 1.0f);

                    var results = new RaycastHit2D[1];
                    if (Physics2D.Raycast(origin, _lastDirection, _contactFilter, results, interactionRadius) > 0)
                    {
                        var hit = results[0];
                        var selectableObject = hit.collider.gameObject;
                        var selectable = selectableObject.GetComponent<Selectable>();

                        if (selectable && selectableObject != _selectedObject)
                        {
                            if (_selectedObject)
                            {
                                _selectedObject.GetComponent<Selectable>().Deselect();
                            }

                            _selectedObject = selectableObject;
                            selectable.Select();
                        }
                    }
                    else
                    {
                        _selectedObject?.GetComponent<Selectable>().Deselect();
                        _selectedObject = null;
                    }
                }
                else if (_isWalking)
                {
                    StopWalking();
                }
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
            _lastDirection = _movement.normalized;
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
