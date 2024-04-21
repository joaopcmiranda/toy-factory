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
        private float updateInterval = 0.5f; // Time in seconds between updates
        private float timer; // Timer to keep track of interval

        private void Start()
        {
            foreach (var item in items)
            {
                _items.Add(new Tuple<GameObject, Item>(item, item.GetComponent<Item>()));
            }
        }

        private Item _previouslyHighlightedItem;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                HighlightNearestItemWithinRadius(target);
                timer = 0;
            }
        }

        public Tuple<GameObject, Item> GetNearestItemTupleWithinDropRadius(Transform target)
        {
            Tuple<GameObject, Item> nearestItem = null;
            var nearestDistance = Mathf.Infinity;

            foreach (var item in _items)
            {
                var distance = Vector2.Distance(item.Item1.transform.position, target.position);
                var itemComponent = item.Item2;

                if (itemComponent && distance <= dropRadius && distance < nearestDistance)
                {
                    nearestItem = item;
                    nearestDistance = distance;
                }
            }
            return nearestItem;
        }


        public Item HighlightNearestItemWithinRadius(Transform target)
        {

            var nearestItemTuple = GetNearestItemTupleWithinDropRadius(target);
            var nearestItemComponent = nearestItemTuple?.Item2;
            var nearestItem = nearestItemTuple?.Item1;


            if (nearestItemComponent != _previouslyHighlightedItem)
            {
                if (_previouslyHighlightedItem)
                {
                    _previouslyHighlightedItem.SetItemColor(Color.white);
                }

                if (nearestItem)
                {
                    nearestItemComponent.SetItemColor(Color.grey);
         
                    _previouslyHighlightedItem = nearestItemComponent;
                }
                else
                {
                    _previouslyHighlightedItem = null;
                }
            }
            return nearestItemComponent;
        }

        public void RefreshItems()
        {
            _items.Clear();
            var allItems = FindObjectsOfType<Item>();
            //Debug.Log($"Found {allItems.Length} items in the scene.");
            foreach (var item in allItems)
            {
                _items.Add(new Tuple<GameObject, Item>(item.gameObject, item));
            }
        }

    }
}
