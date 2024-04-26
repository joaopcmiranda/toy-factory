using System.Collections;
using System.Collections.Generic;
using System.Linq;
using items;
using recipes;
using score;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    private readonly List<Order> _orders = new List<Order>();

    public GameObject orderPrefab;
    public Transform orderEntryPoint;
    public bool singleOrderLevel;

    private readonly List<IRecipe> _recipes = new List<IRecipe>();
    private ScoreManager _scoreManager;
    public GameObject dropBoxObject;


    public void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        if (singleOrderLevel)
        {
            CreateNewOrder();
        }
        else
        {
            StartOrderCoroutine();
        }
    }

    private void StartOrderCoroutine()
    {
        StartCoroutine(LaunchOrderLoop());
    }

    private IEnumerator LaunchOrderLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Wait for 1 second

            CreateNewOrder();
        }
    }

    private IRecipe GetRecipe(bool force = false)
    {
        // Recipes in _recipes have a frequency (per minute) property. should pick from the list of recipes based on frequency
        // Example:
        // R1 - 1 ( /60 )
        // R2 - 2 ( /60 )
        // R3 - 1 ( /60 )
        // Frequencies per call of this function should be 1/60, 2/60, 1/60 and the rest return nothing;

        int totalFrequency = _recipes.Sum(recipe => recipe.frequency);

        int random = Random.Range(0, force ? totalFrequency : 60);
        foreach (var recipe in _recipes)
        {
            random -= recipe.frequency;
            if (random <= 0)
            {
                return recipe;
            }
        }

        return null;
    }

    private void CreateNewOrder(bool force = false)
    {
        if (_recipes.Count == 0)
        {
            return;
        }
        var recipe = singleOrderLevel ? _recipes.First() : GetRecipe(force);

        if (recipe == null)
        {
            return;
        }

        var orderObj = Instantiate(orderPrefab, orderEntryPoint.position, orderEntryPoint.rotation, transform);
        var order = orderObj.GetComponent<Order>();
        order.SetRecipe(recipe);
        order.queuePosition = _orders.Count;

        dropBoxObject.transform.SetParent(order.transform);

        BoxCollider2D boxCollider = dropBoxObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;

        Vector3 newPosition = new Vector3(0f, 1f, 0f);
        dropBoxObject.transform.localPosition = newPosition;
 
        _orders.Add(order);


    }

    public bool FinishOrder(Item delivery)
    {
        var order = _orders.Find(order => order.deliveryItem == delivery.type);

        if (!order)
            return false;

        _scoreManager.IncreaseScore(order.remainingPoints);

        RemoveOrder(order);

        return true;
    }

    private void UpdateItemQueuePosition()
    {
        for (int i = 0; i < _orders.Count; i++)
        {
            _orders[i].queuePosition = i;
        }
    }

    public void OrderExpired(Order order)
    {
        RemoveOrder(order);
        _scoreManager.DecreaseScore(100);
        if (_orders.Count == 0)
        {
            CreateNewOrder(true);
        }
    }

    private void RemoveOrder(Order order)
    {
        _orders.Remove(order);
        var go = order.gameObject;
        Destroy(order);
        Destroy(go);
        if (_orders.Count == 0)
        {
            CreateNewOrder(true);
        }
        UpdateItemQueuePosition();
    }

    public void RegisterRecipe(IRecipe recipe)
    {
        _recipes.Add(recipe);
    }
}
