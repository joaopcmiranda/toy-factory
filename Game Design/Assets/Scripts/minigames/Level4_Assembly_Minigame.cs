using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_Assembly_Minigame : Level4_minigame
{
    public GameObject movingBar;
    public GameObject indicator;
    public GameObject background;

    private float barSpeed;
    private float changeInterval = 2f;
    private float minY = -0.42f;
    private float maxY = 0.25f;

    private int successes = 0;
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
        barSpeed = Random.Range(0.3f, 1f) * (Random.Range(0, 2) * 2 - 1);
        StartCoroutine(RandomSpeedChange());
    }

    public override void EndGame()
    {
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

    IEnumerator RandomSpeedChange()
    {
        while (true)
        {
            float newSpeed = Random.Range(0.6f, 1.4f);
            barSpeed = Mathf.Sign(barSpeed) * newSpeed;
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private void MoveIndicator()
    {
        if (successes >= 4)
        {
            audioManager.PlayMachineComplete();
            EndGame();
            machine.TransformItem(machine.getItem());
        }
        float randomY = Random.Range(0.3f, -0.43f);
        indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, randomY, indicator.transform.localPosition.z);
    }

    public override void minigame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isClickable)
            {
                audioManager.PlayNailHammer();
                successes++;
                MoveIndicator();
            }
        }
        else
        {
            audioManager.PlayBreakItem();          
        }

        movingBar.transform.Translate(0, barSpeed * Time.deltaTime, 0);

        if (movingBar.transform.localPosition.y > maxY || movingBar.transform.localPosition.y < minY)
        {
            barSpeed = -Mathf.Abs(barSpeed) * Mathf.Sign(movingBar.transform.localPosition.y - maxY);
            movingBar.transform.localPosition = new Vector3(movingBar.transform.localPosition.x,
            Mathf.Clamp(movingBar.transform.localPosition.y, minY, maxY), movingBar.transform.localPosition.z);
        }
    }

    public override void GameVisibility(bool isGameVisible)
    {
        movingBar.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        indicator.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        background.GetComponent<SpriteRenderer>().enabled = isGameVisible;
    }
}
