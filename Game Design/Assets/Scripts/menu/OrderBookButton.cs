using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderBookButton : MonoBehaviour
{

    public Image orderBook;
    public Image secondaryOrder;

    public Text Disclaimer;
    public Text Assembly;
    public Text Carving;
    public Text Chopping;
    public Text Painting;

    public Image background; 

    private bool bookOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        orderBook.enabled = false;
        Disclaimer.enabled = false;
        Assembly.enabled = false;
        Carving.enabled = false;
        Chopping.enabled = false;
        Painting.enabled = false;

        background.enabled = false;


        if (secondaryOrder)
        {
            secondaryOrder.enabled = false;
        }
    }

    public void ClickButton()
    {
        bookOpen = !bookOpen;
        orderBook.enabled = bookOpen;
        Disclaimer.enabled = bookOpen;
        Assembly.enabled = bookOpen;
        Carving.enabled = bookOpen;
        Chopping.enabled = bookOpen;
        Painting.enabled = bookOpen;

        background.enabled = bookOpen;


        if (secondaryOrder)
        {
            secondaryOrder.enabled = bookOpen;
        }
    }
}
