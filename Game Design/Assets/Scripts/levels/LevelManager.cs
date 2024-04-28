using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace managers
{
    public class LevelManager : MonoBehaviour
    {

        private int _currentScene;
        private int _overlayScene;
        private int _levelScene; //level that the player is or have played in

        private void Start()
        {
            LoadMainMenu();
        }

        private void Update()
        {
            //can be deleted but just to short-cut the game
            //instant win the level by pressing the 'Return' key
            if (Input.GetKeyUp(KeyCode.Escape) && _levelScene > -1)  LoadAfterLevelPlayed();
        }

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
            _levelScene = -1;
        }

        public void LoadLevel0()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            _currentScene = 2;
            _levelScene = 0;
        }

        public void LoadLevel1()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            _currentScene = 3;
            _levelScene = 1;
        }

        public void LoadLevel2()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            _currentScene = 4;
            _levelScene = 2;
        }

        public void LoadLevel3()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            _currentScene = 5;
            _levelScene = 3;
        }

        public void LoadLevel4()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(6, LoadSceneMode.Additive);
            _currentScene = 6;
            _levelScene = 4;
        }

        public void LoadAfterLevelPlayed()
        {
            if (_levelScene == 4)
            {
                //loads the end of the game after the final level

                //meant to change the canvas of the final LevelEnd Scene to have only a
                //'Back to Menu' and 'Exit', but i can't get the canvas objects for some
                //reason so here's the alternative
                LoadGameEnd();
            }
            else
            {
                LoadLevelEnd();
            }
        }

        private void LoadLevelEnd()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(7, LoadSceneMode.Additive);
            _currentScene = 7;
        }

        private void LoadGameEnd()
        {
            UnloadCurrentScene();

            SceneManager.LoadScene(8, LoadSceneMode.Additive);
            _currentScene = 8;
        }

        public int GetLevelScene() {  return _levelScene; }
    }
}
