using System.Collections;
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

    public int successes = 0;

    public bool isClickable; 


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
            //float randomY = Random.Range(0.3f, -0.43f);
            //indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, 1f, indicator.transform.localPosition.z);
            StartCoroutine(MoveIndicatorCoroutine());
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isClickable)
            {
                StopAllCoroutines(); // Stop the coroutine if E is released
                ResetIndicatorPosition();
                successes++;
                audioManager.PlayNailHammer();
                if (successes >= 3)
                {
                    machine.TransformItem(machine.getItem());
                    audioManager.PlayMachineComplete();

                    EndGame(); 
                }
                //indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, -0.43f, indicator.transform.localPosition.z);
                //reset position
            }
            else
            {
                StopAllCoroutines(); // Stop the coroutine if E is released
                ResetIndicatorPosition();
                //indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, -0.43f, indicator.transform.localPosition.z);
                //reset position 
            }    

            Debug.Log("Released");
        }

    }

    IEnumerator MoveIndicatorCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = indicator.transform.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x, 0.3f, startPosition.z);
        float moveDuration = 1f; // Adjust the duration as needed

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            indicator.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }

    void ResetIndicatorPosition()
    {
        indicator.transform.localPosition = new Vector3(indicator.transform.localPosition.x, -0.43f, indicator.transform.localPosition.z);
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
