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
