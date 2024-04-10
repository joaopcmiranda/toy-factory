using machines;
using managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menu
{
    public class MainMenu : MonoBehaviour
    {
        //start unity in Game scene
        private LevelManager level;

        void Start()
        {
            level = GameObject.Find("Managers").GetComponent<LevelManager>();
        }

        void Update() {}

        public void OnStartButton()
        {
            level.LoadLevel0();
        }

        public void OnOptionsButton() { }

        public void OnExitButton() { }

        public void OnNextLevelButton() { }
    }
}
