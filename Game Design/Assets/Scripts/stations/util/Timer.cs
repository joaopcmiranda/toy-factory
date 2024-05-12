using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float time;
    public TextMeshProUGUI timerText;
    public Image fill;
    public GameObject timerCanvas;
    public float max;

    private bool _timerActive;
    private AudioManager _audioManager;

    private void Start()
    {
        ShowTimer(false);
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartTimer(float duration)
    {
        time = duration;
        _timerActive = true;
        ShowTimer(true);
    }

    public bool IsActive()
    {
        return _timerActive;
    }

    public bool IsTimeUp()
    {
        return time <= 0;
    }

    public void ResetTimer()
    {
        _timerActive = false;
        time = 0;
        ShowTimer(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0) return;

        time -= Time.deltaTime;
        UpdateTimerUI();

        if (time <= 0)
        {
            _audioManager.PlayMachineComplete();
            time = 0;
            ShowTimer(false);
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = "" + (int)time;
        fill.fillAmount = time / max;
    }

    private void ShowTimer(bool show) => timerCanvas.SetActive(show);
}
