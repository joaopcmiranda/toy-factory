using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float time;
    public TextMeshProUGUI timerText;
    public Image fill;
    public Image outer;
    private bool timerActive = false;
    public float max;

    private void Start()
    {
        ShowTimer(false);
    }

    public void StartTimer(float duration)
    {
        time = duration;
        timerActive = true;
        ShowTimer(true);
    }

    public bool IsTimeUp()
    {
        return !timerActive;
    }

    public void ResetTimer()
    {
        timerActive = false;
        time = 0;
        ShowTimer(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerActive) return;

        time -= Time.deltaTime;
        UpdateTimerUI();

        if (time <= 0)
        {
            time = 0;
            timerActive = false;
            ShowTimer(false);
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = "" + (int)time;
        fill.fillAmount = time / max;
    }

    private void ShowTimer(bool show)
    {
        timerText.enabled = show;
        fill.enabled = show;
        outer.enabled = show;
    }
}
