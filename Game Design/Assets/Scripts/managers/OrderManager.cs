using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text orderText;
    public int orderNumber = 1; 

    public Text timerTextOne;
    public Text timerTextTwo;
    public Text timerTextThree;
    public Text timerTextFour;

    public Text scoreText;
    public int score; 

    // Start is called before the first frame update
    void Start()
    {
        orders = new Order[1];
        timerTexts = new Text[1];

        score = 0;
        scoreText.text = "Score: " + score.ToString(); 

        timerTexts[0] = timerTextOne;
        //timerTexts[1] = timerTextTwo;
        //timerTexts[2] = timerTextThree;
        //timerTexts[3] = timerTextFour;

        for (int i = 0; i < orders.Length; i++)
        {
            orders[i] = new Order("Order " + (i + 1), Random.Range(10.0f, 20.0f), timerTexts[i]);
        }

    }

    // Update is called once per frame
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

    public void ReplaceOrder(int index)
    {
        orders[index] = new Order("New Order", Random.Range(10.0f, 30.0f), timerTexts[index]);
        orderText.text = "Score: " + ++orderNumber; 
    }

    public void CreateNewOrder(int index)
    {
        decreaseScore(100);
        orders[index] = new Order("New Order", Random.Range(10.0f, 30.0f), timerTexts[index]);
        orderText.text = "Score: " + ++orderNumber; 
    }

    public Order[] getOrders()
    {
        return orders; 
    }

    public void increaseScore(int i)
    {
        score += i;
        updateScore(score);
    }

    public void decreaseScore(int i)
    {
        score -= i;
        updateScore(score);
    }

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
