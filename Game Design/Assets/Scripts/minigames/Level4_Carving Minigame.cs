using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_CarvingMinigame : Level4_minigame
{
    public GameObject movingBar;
    public GameObject indicator;
    public GameObject background;

    private float tapSpeed = 0.1f;
    private float progressNeeded = 0.3f; 
    private float currentProgress = -0.43f; 

    private bool isClickable;

    public override void Start()
    {
        GameVisibility(false);
        //StartGame(); 
    }

    public override void StartGame()
    {
        gameEnabled = true;
        gameStarted = true;
        GameVisibility(true);
    }

    public override void EndGame()
    {
        GameVisibility(false);
        gameEnabled = false;
        gameStarted = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entering!");
        if (other.gameObject == indicator)
        {

            isClickable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exiting!");
        if (other.gameObject == indicator)
        {
            isClickable = false;
        }
    }

    public override void minigame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tapped!");

            audioManager.PlayNailHammer(); 

            currentProgress += tapSpeed;
            indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, currentProgress, indicator.transform.localPosition.z);

            //update the indicator
            if (currentProgress >= progressNeeded)
            {
                machine.TransformItem(machine.getItem());
                audioManager.PlayMachineComplete();

                currentProgress = 0f;
                indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, -0.43f, indicator.transform.localPosition.z);

                EndGame();
            }
        }
    }

    public override void GameVisibility(bool isGameVisible)
    {
        movingBar.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        indicator.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        background.GetComponent<SpriteRenderer>().enabled = isGameVisible;
    }
}
