using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject movementPanel;
    public GameObject recipePanel;
    public GameObject pickupsPanel;
    public GameObject deliveryPanel;

    //reactivating other UI
    public GameObject orderBookUI;
    public GameObject scoreAndTimer;
    public LevelTimer levelTimer;
    public score.ScoreManager scoreManager;

    private int _currentPanel = 0;


    private void Start()
    {
        movementPanel.SetActive(true);
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
                    recipePanel.SetActive(true);
                    orderBookUI.SetActive(true);
                    _currentPanel++;
                    break;
                case 1:
                    recipePanel.SetActive(false);
                    pickupsPanel.SetActive(true);
                    _currentPanel++;
                    break;
                case 2:
                    pickupsPanel.SetActive(false);
                    deliveryPanel.SetActive(true);
                    _currentPanel++;
                    break;
                case 3:
                    deliveryPanel.SetActive(false);
                    scoreAndTimer.SetActive(true);
                    levelTimer.StartTimer(levelTimer.time);
                    scoreManager.StartScore();
                    _currentPanel++;
                    break;
            }
        }
    }
}
