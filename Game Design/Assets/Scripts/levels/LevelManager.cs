using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace managers
{
    public class LevelManager : MonoBehaviour
    {
        private int _currentScene;
        private int _overlayScene;
        private int _levelScene; 

        // Lists to manage scene indices
        private List<int> levelScenes;
        private List<int> cutSceneIndices;
        private List<int> levelEndSceneIndices;

        private void Start()
        {
            InitializeSceneIndices();
            LoadMainMenu();
        }

        private void InitializeSceneIndices()
        {
            levelScenes = new List<int> { 2, 4, 6, 8, 10 };
            cutSceneIndices = new List<int> { 3, 5, 7, 9, 11 };
            levelEndSceneIndices = new List<int> { 12, 12, 12, 12, 12 };
        }

        public int GetLevelScene()
        {
            return _levelScene;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && _levelScene > -1)
            {
                LoadAfterLevelPlayed();
            }
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

        public void LoadLevel(int levelIndex)
        {
            UnloadCurrentScene();
            int sceneIndex = levelScenes[levelIndex];
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
            _currentScene = sceneIndex;
            _levelScene = levelIndex;
        }

        public void LoadAfterLevelPlayed()
        {
            StartCoroutine(LoadLevelCutscene());
        }

        private IEnumerator LoadLevelCutscene()
        {
            UnloadCurrentScene();

            int cutSceneIndex = cutSceneIndices[_levelScene];
            SceneManager.LoadScene(cutSceneIndex, LoadSceneMode.Additive);
            _currentScene = cutSceneIndex;

            yield return new WaitForSeconds(5);  // Wait for 5 seconds before unloading

            SceneManager.UnloadSceneAsync(_currentScene);

            if (_levelScene == 4)
            {
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

            int levelEndSceneIndex = levelEndSceneIndices[_levelScene];
            SceneManager.LoadScene(levelEndSceneIndex, LoadSceneMode.Additive);
            _currentScene = levelEndSceneIndex;
        }

        private void LoadGameEnd()
        {
            UnloadCurrentScene();
            SceneManager.LoadScene(13, LoadSceneMode.Additive);
            _currentScene = 13;
        }
    }
}


