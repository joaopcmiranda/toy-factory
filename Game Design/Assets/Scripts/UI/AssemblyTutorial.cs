using UnityEngine;

public class AssemblyTutorial : MonoBehaviour
{

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    //reactivating other UI

    private int _currentPanel = 0;
    private bool tutorialDone = false;
    private bool controlsEnabled = true;

    public void StartTutorial()
    {
        if (!tutorialDone)
        {
            panel1.SetActive(true);
            EnableDisableControls();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!tutorialDone && _currentPanel < 3 && Input.GetKeyDown(KeyCode.Return))
        {
            switch (_currentPanel)
            {
                case 0:
                    panel1.SetActive(false);
                    panel2.SetActive(true);
                    _currentPanel++;
                    break;
                case 1:
                    panel2.SetActive(false);
                    panel3.SetActive(true);
                    _currentPanel++;
                    break;
                case 2:
                    panel3.SetActive(false);
                    _currentPanel++;
                    tutorialDone = true;
                    EnableDisableControls(); // Disable tutorial controls once complete
                    break;
            }
        }
    }

    public void EnableDisableControls()
    {
        controlsEnabled = !controlsEnabled;
    }
}
