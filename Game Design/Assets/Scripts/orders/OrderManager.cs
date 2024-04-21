
using System.Collections;
using System.Collections.Generic;
using items;
using recipes;
using score;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    private List<Order> orders;

    public Text orderText;
    public int orderNumber = 1;

    public GameObject orderPrefab;
    public Transform orderEntryPoint;

    private List<IRecipe> _recipes;
    private ScoreManager _scoreManager;


    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void StartOrderCoroutine()
    {
        StartCoroutine(LaunchOrderLoop());
    }

    private IEnumerator LaunchOrderLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Wait for 1 second

            CreateNewOrder(GetRecipe());
        }
    }

    private IRecipe GetRecipe()
    {
        // Recipes in _recipes have a frequency (per minute) property. should pick from the list of recipes based on frequency
        // Example:
        // R1 - 1 ( /60 )
        // R2 - 2 ( /60 )
        // R3 - 1 ( /60 )
        // Frequencies per call of this function should be 1/60, 2/60, 1/60 and the rest return nothing;

        int random = Random.Range(0, 60);
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

    public void CreateNewOrder(IRecipe recipe)
    {
        var orderObj = Instantiate(orderPrefab, orderEntryPoint.position, orderEntryPoint.rotation, transform);
        var order = orderObj.GetComponent<Order>();
        order.SetRecipe(recipe);

    }

    public bool FinishOrder(Item delivery)
    {
        foreach (var order in orders)
        {
            if (order.recipe.deliveryItem == delivery.type)
            {
                _scoreManager.IncreaseScore(order.remainingPoints);
                orders.Remove(order);
                Destroy(order.gameObject);
                return true;
            }
        }

        return false;
    }

    public void OrderExpired(Order order)
    {
        orders.Remove(order);
        Destroy(order.gameObject);
        _scoreManager.DecreaseScore(100);
    }

    public void RegisterRecipe(IRecipe recipe)
    {
        _recipes.Add(recipe);
    }
}
