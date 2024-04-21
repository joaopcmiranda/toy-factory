using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using recipes;
using score;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    public Order[] orders;
    public Text[] timerTexts;

    public Text orderText;
    public int orderNumber = 1;

    public Text timerTextOne;
    public Text timerTextTwo;
    public Text timerTextThree;
    public Text timerTextFour;

    private List<IRecipe> _recipes;
    private ScoreManager _scoreManager;


    void Start()
    {
        _scoreManager = gameObject.GetComponent<ScoreManager>();

        orders = new Order[1];
        timerTexts = new Text[1];

        timerTexts[0] = timerTextOne;
        //timerTexts[1] = timerTextTwo;
        //timerTexts[2] = timerTextThree;
        //timerTexts[3] = timerTextFour;

        for (int i = 0; i < orders.Length; i++)
        {
            orders[i] = new Order("Order " + (i + 1), Random.Range(50.0f, 80.0f), timerTexts[i]);
        }

        StartOrderCoroutine();

    }


    void Update()
    {
        //ADD A DELAY TO GENERATING THE ORDERS
        for (int i = 0; i < orders.Length; i++)
        {
            orders[i].timeLeft -= Time.deltaTime;
            if (orders[i].isExpired()) CreateNewOrder(i);

            orders[i].UpdateTimerText();
        }
        //IF ITEM IS DROPPED ON OUTBOX, CHECK IF IT IS A CURRENT ORDER
    }

    public void StartOrderCoroutine()
    {
        StartCoroutine(LaunchOrder());
    }

    private IEnumerator LaunchOrder()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);  // Wait for 1 second

            // Find index of first order with expired time
            int index = Array.FindIndex(orders, order => order.isExpired());
            if (index != -1)
            {
                CreateNewOrder(index);
            }
        }
    }

    public void RegisterRecipe(IRecipe recipe)
    {
        _recipes.Add(recipe);
    }

    public void ReplaceOrder(int index)
    {
        orders[index] = new Order("New Order", Random.Range(50.0f, 80.0f), timerTexts[index]);
        orderText.text = "Order: " + ++orderNumber;
    }

    public void CreateNewOrder(int index)
    {
        _scoreManager.DecreaseScore(100);
        orders[index] = new Order("New Order", Random.Range(50.0f, 80.0f), timerTexts[index]);
        orderText.text = "Order: " + ++orderNumber;
    }

    public Order[] getOrders()
    {
        return orders;
    }
}
