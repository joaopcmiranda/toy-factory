using System;
using System.Collections.Generic;
using items;
using UnityEngine;
using UnityEngine.Serialization;

namespace managers
{
    public class ItemManager : MonoBehaviour
    {
        public List<GameObject> itemPrefabs;
        private readonly List<Tuple<GameObject, Item>> _items = new List<Tuple<GameObject, Item>>();
        public float dropRadius;

        public Transform target; //player
        private float updateInterval = 0.2f; // Time in seconds between updates
        private float timer; // Timer to keep track of interval

        private List<Tuple<GameObject, Item>> itemsInRadius = new List<Tuple<GameObject, Item>>();
        private Item _previouslyHighlightedItem;

        private void Start()
        {
            foreach (var item in itemPrefabs)
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
                if (item.Item1 == null || item.Item2 == null || item.Item1.Equals(null))
                {
                    continue; // Skip if the GameObject or the Item component is null or destroyed
                }

                var distance = Vector2.Distance(item.Item1.transform.position, target.position);
                var itemComponent = item.Item2;
                if (distance <= dropRadius && distance < nearestDistance)
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

            bool foundItemInRadius = false;

            if (hit.collider != null)
            {
                Item itemComponent = hit.collider.GetComponent<Item>();
                if (itemComponent == null) return;

                foreach (var item in itemsInRadius)
                {
                    if (item.Item2 == null) continue;

                    foundItemInRadius = true;
                    if (item.Item2 != _previouslyHighlightedItem)
                    {
                        if (_previouslyHighlightedItem)
                        {
                            _previouslyHighlightedItem.SetItemColor(Color.white);
                        }

                        item.Item2.SetItemColor(Color.grey);
                        _previouslyHighlightedItem = item.Item2;
                    }

                    else
                    {
                        item.Item2.SetItemColor(Color.white);
                    }
                }
            }

            if (!foundItemInRadius)
            {
                // Dehighlight any previously highlighted item if it is no longer in the radius
                if (_previouslyHighlightedItem)
                {
                    _previouslyHighlightedItem.SetItemColor(Color.white);
                    _previouslyHighlightedItem = null;
                }
            }
        }

        public void RefreshItems()
        {
            _items.Clear();
            var allItems = FindObjectsOfType<Item>();
            foreach (var item in allItems)
            {
                if (item != null && item.gameObject != null) // Ensure the item and its GameObject are not null
                {
                    _items.Add(new Tuple<GameObject, Item>(item.gameObject, item));
                }
            }
        }

        public void PreRemoveItem(Item item)
        {
            if (_previouslyHighlightedItem == item)
            {
                _previouslyHighlightedItem = null;
            }

            _items.RemoveAll(t => t.Item2 == item);
            itemsInRadius.RemoveAll(t => t.Item2 == item);

            RefreshItems();
        }

        public Item CreateItem(ItemType type, Transform holdSpot)
        {
            var itemObject = Instantiate(itemPrefabs.Find(i => i.GetComponent<Item>().type == type), Vector3.zero, Quaternion.identity, holdSpot);
            var item = itemObject.GetComponent<Item>();
            _items.Add(new Tuple<GameObject, Item>(itemObject, item));
            return item;
        }

    }
}
