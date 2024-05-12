using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level4_PaintingMinigame : Level4_minigame
{
    public GameObject movingBar;
    public GameObject indicator;
    public GameObject background;

    private bool isClickable;
    public float checkDuration = 3f;
    public float timer = 0f;
    private bool isChecking = false;

    private float barSpeed;
    private float minY = -0.42f;
    private float maxY = 0.25f;

    public override void Start()
    {
        GameVisibility(false);
    }

    public override void StartGame()
    {
        gameEnabled = true;
        gameStarted = true;
        GameVisibility(true);
        barSpeed = .5f;
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
        if (other.gameObject == indicator)
        {

            isClickable = true;
            StartVariableCheck();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == indicator)
        {
            isClickable = false;
        }
    }

    public override void minigame()
    {
        //movingBar pick a random point
        //
        if (Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            StartCoroutine(MoveIndicatorUpCoroutine());
            Debug.Log("Pressed!");
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            StopAllCoroutines();
            //StopCoroutine(MoveIndicatorUpCoroutine());
            StartCoroutine(MoveIndicatorDownCoroutine());
            //isMovingDown = true;
            //isMovingUp = false;
        }


        if (isChecking)
        {
            timer += Time.deltaTime;

            if (timer >= checkDuration)
            {
                // If the variable remains true for the specified duration, perform actions
                machine.TransformItem(machine.getItem());
                audioManager.PlayMachineComplete();
                EndGame();
                isChecking = false;
            }
            else if (!isClickable)
            {
                // If the variable turns false before the check duration elapses, stop checking
                isChecking = false;
            }
        }

        movingBar.transform.Translate(0, barSpeed * Time.deltaTime, 0);

        if (movingBar.transform.localPosition.y > maxY || movingBar.transform.localPosition.y < minY)
        {
            barSpeed = -Mathf.Abs(barSpeed) * Mathf.Sign(movingBar.transform.localPosition.y - maxY);
            movingBar.transform.localPosition = new Vector3(movingBar.transform.localPosition.x,
            Mathf.Clamp(movingBar.transform.localPosition.y, minY, maxY), movingBar.transform.localPosition.z);
        }


    }

    void StartVariableCheck()
    {
        isChecking = true;
        timer = 0f;
    }

    IEnumerator MoveIndicatorUpCoroutine()
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

    IEnumerator MoveIndicatorDownCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = indicator.transform.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x, -0.42f, startPosition.z);
        float moveDuration = 1f; // Adjust the duration as needed

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            indicator.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }

    public override void GameVisibility(bool isGameVisible)
    {
        movingBar.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        indicator.GetComponent<SpriteRenderer>().enabled = isGameVisible;
        background.GetComponent<SpriteRenderer>().enabled = isGameVisible;
    }
}
