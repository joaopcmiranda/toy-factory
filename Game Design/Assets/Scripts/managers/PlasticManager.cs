using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticManager : MonoBehaviour
{

    public Sprite trainSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "3DPrinter") 
        {

            spriteRenderer.sprite = trainSprite;
        }
    }
}
