using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderBookButtonLevel2 : MonoBehaviour
{

    public Image orderBook;
    public Image secondaryOrder;

    private bool bookOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        orderBook.enabled = false;

        if (secondaryOrder)
        {
            secondaryOrder.enabled = false;
        }
    }

    public void ClickButton()
    {
        bookOpen = !bookOpen;
        orderBook.enabled = bookOpen;

        if (secondaryOrder)
        {
            secondaryOrder.enabled = bookOpen;
        }
    }
}
