using managers;
using UnityEngine;

namespace menu
{
    public class Menu : MonoBehaviour
    {
        private LevelManager levelManager;

        void Start()
        {
            levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();
            if (levelManager == null)
            {
                Debug.LogError("LevelManager component not found on 'Managers' GameObject!");
            }
        }

        void Update() { }

        public void OnTutorialButton()
        {
            levelManager.LoadLevel(0);
        }

        public void OnLevel1Button()
        {
            levelManager.LoadLevel(1); 
        }

        public void OnLevel2Button()
        {
            levelManager.LoadLevel(2);
        }

        public void OnLevel3Button()
        {
            levelManager.LoadLevel(3);
        }

        public void OnLevel4Button()
        {
            levelManager.LoadLevel(4);
        }

        public void OnOptionsButton()
        {
            // Implement options menu functionality here
        }

        public void OnExitButton()
        {
            // This function properly works when built as a standalone application
            Application.Quit();
        }

        public void OnNextLevelButton()
        {
            int currentLevel = levelManager.GetLevelScene();
            if (currentLevel >= 0 && currentLevel < 4)  // Ensure there is a next level to load
            {
                levelManager.LoadLevel(currentLevel + 1);
            }
        }

        public void OnMenuButton()
        {
            levelManager.LoadMainMenu();
        }
    }
}

