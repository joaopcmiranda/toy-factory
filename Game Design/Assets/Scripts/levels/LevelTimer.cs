using UnityEngine;
using UnityEngine.UI;
using managers;

public class LevelTimer : MonoBehaviour
{

    public float time;
    public Text timerText;
    private LevelManager level;
    private AudioManager audioManager;

    private void Start()
    {
        //StartTimer(time);
        level = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartTimer(float duration)
    {
        time = duration;
        level = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 1)
        {
            //audioManager.PlayLevelComplete();
            //SceneManager.LoadScene("LevelEnd");
            level.LoadAfterLevelPlayed();
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
