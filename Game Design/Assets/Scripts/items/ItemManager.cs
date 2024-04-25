using System;
using System.Collections.Generic;
using items;
using UnityEngine;

namespace managers
{
    public class ItemManager : MonoBehaviour
    {
        public List<GameObject> items;
        private readonly List<Tuple<GameObject, Item>> _items = new List<Tuple<GameObject, Item>>();
        public float dropRadius;

        public Transform target; //player
        private float updateInterval = 0.2f; // Time in seconds between updates
        private float timer; // Timer to keep track of interval

        private List<Tuple<GameObject, Item>> itemsInRadius = new List<Tuple<GameObject, Item>>();
        private Item _previouslyHighlightedItem;

        private void Start()
        {
            foreach (var item in items)
            {
                _items.Add(new Tuple<GameObject, Item>(item, item.GetComponent<Item>()));
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                HighlightMousedOverItemWithinRadius(target);
                timer = 0;
            }
        }

        public void GetNearestItemTuplesWithinDropRadius(Transform target)
        {
            itemsInRadius.Clear();
            var nearestDistance = Mathf.Infinity;
            foreach (var item in _items)
            {
                var distance = Vector2.Distance(item.Item1.transform.position, target.position);
                var itemComponent = item.Item2;
                if (itemComponent && distance <= dropRadius && distance < nearestDistance)
                {
                    itemsInRadius.Add(item);
                }
            }
        }


        public void HighlightMousedOverItemWithinRadius(Transform target)
        {
            GetNearestItemTuplesWithinDropRadius(target);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Item itemComponent = hit.collider.GetComponent<Item>();

                foreach (var item in itemsInRadius)
                {
                    if (item.Item2 == itemComponent)
                    {
                        if (item.Item2 != _previouslyHighlightedItem)
                        {
                            if (_previouslyHighlightedItem)
                            {
                                _previouslyHighlightedItem.SetItemColor(Color.white);
                            }

                            item.Item2.SetItemColor(Color.grey);
                            _previouslyHighlightedItem = item.Item2;
                        }
                    }
                    else
                    {
                        item.Item2.SetItemColor(Color.white);
                    }
                }
            }
            else
            {
                foreach (var item in itemsInRadius)
                {
                    item.Item2.SetItemColor(Color.white);
                }
                _previouslyHighlightedItem = null;
            }
        }

        public void RefreshItems()
        {
            _items.Clear();
            var allItems = FindObjectsOfType<Item>();
            foreach (var item in allItems)
            {
                _items.Add(new Tuple<GameObject, Item>(item.gameObject, item));
            }
        }

    }
}