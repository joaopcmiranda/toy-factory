using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CraftingStation : Level4_minigame
{
    public GameObject releaseIndicator;
    public GameObject indicator;
    public GameObject background;

    private float barSpeed = 2f;
    private float minY = -0.42f;
    private float maxY = 0.25f;

    public int requiredSuccesses = 3; // Number of successful completions required

    private bool holdingKey = false;
    private float holdTimer = 0f;
    private int successes = 0;

    private bool isClickable; 


    public override void Start()
    {
        GameVisibility(false);
    }

    public override void StartGame()
    {
        gameEnabled = true;
        gameStarted = true;
        GameVisibility(true);
    }

    public override void EndGame()
    {
        //instructionText
        GameVisibility(false);
        gameEnabled = false;
        gameStarted = false;
        successes = 0;
    }

    public override void Update()
    {
        if (gameStarted)
        {
            if (IsGameObjectInRange(machine))
            {
                //Debug.Log("In range");
                GameVisibility(true);
                gameEnabled = true;
            }
            else
            {
                GameVisibility(false);
                gameEnabled = false;
            }

            if (gameEnabled)
            {
                minigame();
            }
        }
        else
        {
            GameVisibility(false);
            gameEnabled = false;
        }
    }

    public override void minigame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //update position of indicator
            Debug.Log("Holding!");
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            //if isclickable increment successes 
            //reset position 
            //else reset position 

            Debug.Log("Released");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == indicator)
        {

            isClickable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == indicator)
        {
            isClickable = false;
        }
    }


    public override void GameVisibility(bool isGameVisible)
    {
        releaseIndicator.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        indicator.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        background.GetComponent<SpriteRenderer>().enabled = isGameVisible;
    }
}
