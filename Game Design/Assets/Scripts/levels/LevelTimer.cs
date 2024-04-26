using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using managers;

public class LevelTimer : MonoBehaviour
{

    public float time;
    public Text timerText;
    private LevelManager level;

    private void Start()
    {
        StartTimer(time);
        level = FindObjectOfType<LevelManager>();
    }

    public void StartTimer(float duration)
    {
        time = duration;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            SceneManager.LoadScene("LevelEnd");
            // level.LoadAfterLevelPlayed();
        } else
        {
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
}
