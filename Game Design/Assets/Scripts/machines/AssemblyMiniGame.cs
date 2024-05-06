using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyMiniGame : MonoBehaviour
{
    public GameObject movingBar;
    public GameObject nail;

    private float barSpeed;
    private float changeInterval = 2f;
    private float minY = -0.42f;
    private float maxY = 0.25f;

    private bool nailClickable;
    private int nailCount = 0;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        barSpeed = Random.Range(0.3f, 1f) * (Random.Range(0, 2) * 2 - 1);
        StartCoroutine(RandomSpeedChange());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (nailClickable)
            {
                nailCount++;
                MoveNail();
            }
            else
            {
                BreakItem();
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

    IEnumerator RandomSpeedChange()
    {
        while (true)
        {
            float newSpeed = Random.Range(0.6f, 1.4f);
            barSpeed = Mathf.Sign(barSpeed) * newSpeed;
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == nail)
        {
            HighlightNail(true);
            nailClickable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nail)
        {
            HighlightNail(false);
            nailClickable = false;
        }
    }

    private void HighlightNail(bool highlight)
    {
        SpriteRenderer spriteRenderer = nail.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlight ? Color.green : Color.white;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on the nail.");
        }
    }

    private void MoveNail()
    {
        if (nailCount >= 3)
        {
            EndGame();
        }
        float randomY = Random.Range(0.3f, -0.43f);
        nail.transform.localPosition = new Vector3(nail.transform.localPosition.x, randomY, nail.transform.localPosition.z);
    }

    private void EndGame()
    {

    }

    private void BreakItem()
    {

    }

}
