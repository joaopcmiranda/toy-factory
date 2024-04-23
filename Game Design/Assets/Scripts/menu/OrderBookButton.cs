using UnityEngine;
using UnityEngine.UI;

public class OrderBookButton : MonoBehaviour
{

    public Image orderBook;

    private bool bookOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        orderBook.enabled = false;
    }

    public void ClickButton()
    {
        bookOpen = !bookOpen;
        orderBook.enabled = bookOpen;
    }
}
