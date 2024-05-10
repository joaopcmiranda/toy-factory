using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float time;
    public TextMeshProUGUI timerText;
    public Image fill;
    public Image outer;
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

    public bool IsTimeUp()
    {
        return !_timerActive;
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
        if (!_timerActive) return;

        time -= Time.deltaTime;
        UpdateTimerUI();

        if (time <= 0)
        {
            _audioManager.PlayMachineComplete();
            time = 0;
            _timerActive = false;
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
