using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct toy
{
    public string tag;
}

public struct Order
{
    public string orderName;
    public float timeLeft;
    public Text timerText;

    //Constructor
    public Order(string name, float time, Text text)
    {
        orderName = name;
        //orderType = REPRESENT BIT BITS? 
        timeLeft = time;
        timerText = text;
    }

    public void UpdateTimerText()
    {
        timerText.text = "Time Left: " + timeLeft.ToString("F1");
    }

    public bool isExpired()
    {
        return timeLeft <= 0;
    }
}

public class OrderManager : MonoBehaviour
{
    public Order[] orders;
    public Text[] timerTexts;

    public Text timerTextOne;
    public Text timerTextTwo;
    public Text timerTextThree;
    public Text timerTextFour;

    // Start is called before the first frame update
    void Start()
    {
        orders = new Order[4];
        timerTexts = new Text[4];

        timerTexts[0] = timerTextOne;
        timerTexts[1] = timerTextTwo;
        timerTexts[2] = timerTextThree;
        timerTexts[3] = timerTextFour;

        for (int i = 0; i < orders.Length; i++)
        {
            orders[i] = new Order("Order " + (i + 1), Random.Range(10.0f, 30.0f), timerTexts[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ADD A DELAY TO GENERATING THE ORDERS
        for (int i = 0; i < orders.Length; i++)
        {
            orders[i].timeLeft -= Time.deltaTime;
            if (orders[i].isExpired()) ReplaceOrder(i);

            orders[i].UpdateTimerText();
        }
        //IF ITEM IS DROPPED ON OUTBOX, CHECK IF IT IS A CURRENT ORDER
    }

    private void ReplaceOrder(int index)
    {
        //SUBTRACT POINTS?
        orders[index] = new Order("New Order", Random.Range(10.0f, 30.0f), timerTexts[index]);
    }

    private void CreateNewOrder(int index)
    {
        //ADD POINTS? 
        orders[index] = new Order("New Order", Random.Range(10.0f, 30.0f), timerTexts[index]);
    }
}
