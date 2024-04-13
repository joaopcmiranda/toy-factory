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
    private bool timerActive = false;
    public float max;

    // Start is called before the first frame update
    public void StartTimer(float duration)
    {
        time = duration;
        timerActive = true;
    }

    public bool IsTimeUp()
    {
        return !timerActive;
    }

    public void ResetTimer()
    {
        timerActive = false;
        time = 0;
        UpdateTimerUI();
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
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = "" + (int)time;
        fill.fillAmount = time / max;
    }
}
