using UnityEngine;
using UnityEngine.SceneManagement;

namespace managers
{
    public class LevelManager : MonoBehaviour
    {

        private int _currentScene;
        private int _overlayScene;

        private void UnloadCurrentScene()
        {
            if (_currentScene != 0)
            {
                SceneManager.UnloadSceneAsync(_currentScene);
            }

            if (_overlayScene != 0)
            {
                SceneManager.UnloadSceneAsync(_overlayScene);
            }
        }

        public void LoadMainMenu()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            _currentScene = 1;
        }

        public void LoadLevel0()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            _currentScene = 2;
        }

        public void LoadLevel1()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            _currentScene = 3;
        }

        public void LoadLevel2()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            _currentScene = 4;
        }

        public void LoadLevel3()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            _currentScene = 5;
        }

        public void LoadLevel4()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(6, LoadSceneMode.Additive);
            _currentScene = 6;
        }

        public void LoadLevelEnd()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(7, LoadSceneMode.Additive);
            _currentScene = 7;
        }

        private void Start()
        {
            LoadMainMenu();
            //LoadLevel0();
        }

    }
}
