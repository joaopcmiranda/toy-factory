using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrinterManager : MonoBehaviour
{

    public TextMeshProUGUI uiText;
    public Transform holdSpot;
    public LayerMask pickUpMask;

    //private GameObject itemHolding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plastic")
        {
            uiText.text = "3D Print Plastic done";
        }
    }
}
