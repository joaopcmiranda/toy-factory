using System.Collections;
using System.Collections.Generic;
using System.Linq;
using items;
using managers;
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
    public int queueSize = 7;
    public int queueStopX = 10;

    private readonly List<IRecipe> _recipes = new List<IRecipe>();
    private ScoreManager _scoreManager;
    private LevelManager _levelManager;


    public void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        if (singleOrderLevel)
        {
            CreateNewOrder(true);
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
        yield return new WaitForSeconds(1);
        CreateNewOrder(true);

        while (true)
        {
            yield return new WaitForSeconds(2);
            CreateNewOrder();
        }
    }

    private IRecipe GetRecipe(bool force = false)
    {
        int totalFrequency = _recipes.Sum(recipe => recipe.frequency);
        int random = Random.Range(0, force ? totalFrequency : 60);
        int threshold = 0;

        foreach (var recipe in _recipes)
        {
            threshold += recipe.frequency;
            if (random < threshold)
            {
                return recipe;
            }
        }

        return null;
    }

    private void CreateNewOrder(bool force = false)
    {
        if (_recipes.Count == 0 || _orders.Count >= queueSize)
        {
            return;
        }
        var recipe = GetRecipe(force);

        if (recipe == null)
        {
            return;
        }

        var orderObj = Instantiate(orderPrefab, orderEntryPoint.position, Quaternion.identity, transform);
        var order = orderObj.GetComponent<Order>();
        order.SetRecipe(recipe);
        order.queuePosition = _orders.Count;
        order.queueStopX = queueStopX;

        _orders.Add(order);

    }

    public bool FinishOrder(Item delivery)
    {
        var order = _orders.Find(o => o.deliveryItem == delivery.type);

        if (order == null)
            return false;

        _scoreManager.IncreaseScore(order.remainingPoints);
        RemoveOrder(order);
        if (_levelManager && _levelManager.GetLevelScene() == 0)
        {
            _levelManager.LoadAfterLevelPlayed();
        }

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
