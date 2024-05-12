using UI;
using UnityEngine;
namespace player
{
    public class SelectionRayCaster : MonoBehaviour
    {
        public Transform rayCaster;
        public float interactionRadius = 1.5f;

        private GameObject _selectedObject;
        private ContactFilter2D _contactFilter;

        private void Start()
        {
            _contactFilter.layerMask = Physics2D.AllLayers;
            _contactFilter.useLayerMask = true;
            _contactFilter.useTriggers = true;

            // Stop the ray from colliding with the current gameobject's collider
            _contactFilter.SetLayerMask(~((1 << gameObject.layer) | (1 << 2)));
        }

        public void CastTorwards(Vector2 direction)
        {
            Vector2 origin = rayCaster.position;
            Debug.DrawRay(origin, direction * interactionRadius, Color.red, 0.1f);

            var results = new RaycastHit2D[1];
            if (Physics2D.Raycast(origin, direction, _contactFilter, results, interactionRadius) > 0)
            {
                var hit = results[0];
                var selectableObject = hit.collider.gameObject;
                var selectable = selectableObject.GetComponent<Selectable>();

                if (selectable && selectableObject != _selectedObject)
                {
                    if (_selectedObject)
                    {
                        var previousSelectable = _selectedObject.GetComponent<Selectable>();
                        if (previousSelectable)
                        {
                            previousSelectable.Deselect();
                        }
                    }

                    _selectedObject = selectableObject;
                    selectable.Select();
                }
            }
            else
            {
                if (_selectedObject)
                {
                    var selectable = _selectedObject.GetComponent<Selectable>();
                    if (selectable)
                    {
                        selectable.Deselect();
                    }
                }
                _selectedObject = null;
            }
        }

        public bool IsObjectSelected()
        {
            return _selectedObject;
        }

        public GameObject GetSelectedObject()
        {
            return _selectedObject;
        }
    }
}
