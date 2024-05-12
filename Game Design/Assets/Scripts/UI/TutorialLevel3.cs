using UnityEngine;

public class TutorialLevel3 : MonoBehaviour
{

    public GameObject movementPanel;
    public GameObject tablePanel;
    public GameObject mixerPanel;
    public GameObject choicePanel;

    //reactivating other UI
    public GameObject orderBookUI;
    public GameObject scoreAndTimer;
    public LevelTimer levelTimer;
    public score.ScoreManager scoreManager;

    private int _currentPanel = 0;


    private void Start()
    {
        movementPanel.SetActive(true);
        levelTimer.PauseTimer();
    }


    // Update is called once per frame
    void Update()
    {
        if (_currentPanel < 4 && Input.GetKeyDown(KeyCode.Return))
        {
            switch (_currentPanel)
            {
                case 0:
                    movementPanel.SetActive(false);
                    tablePanel.SetActive(true);
                    _currentPanel++;
                    break;
                case 1:
                    tablePanel.SetActive(false);
                    mixerPanel.SetActive(true);
                    _currentPanel++;
                    break;
                case 2:
                    mixerPanel.SetActive(false);
                    choicePanel.SetActive(true);
                    _currentPanel++;
                    break;
                case 3:
                    choicePanel.SetActive(false);
                    scoreAndTimer.SetActive(true);
                    orderBookUI.SetActive(true);
                    levelTimer.StartTimer(levelTimer.time);
                    scoreManager.StartScore();
                    _currentPanel++;
                    break;
            }
        }
    }
}
