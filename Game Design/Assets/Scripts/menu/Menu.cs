using machines;
using managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menu
{
    public class Menu : MonoBehaviour
    {
        private LevelManager level;

        void Start()
        {
            level = GameObject.Find("Managers").GetComponent<LevelManager>();
        }

        void Update() {}

        public void OnTutorialButton()
        {
            level.LoadLevel0();
        }

        public void OnLevel1Button()
        {
            level.LoadLevel1();
        }

        public void OnLevel2Button()
        {
            level.LoadLevel2();
        }

        public void OnLevel3Button()
        {
            level.LoadLevel3();
        }

        public void OnLevel4Button()
        {
            level.LoadLevel4();
        }

        public void OnOptionsButton() {}

        public void OnExitButton()
        {
            //it doesn't work in unity, but probably works in the real game
            Application.Quit();
        }

        public void OnNextLevelButton()
        {
            switch (level.GetLevelScene())
            {
                case 0:
                    level.LoadLevel1();
                    break;
                case 1:
                    level.LoadLevel2();
                    break;
                case 2:
                    level.LoadLevel3();
                    break;
                case 3:
                    level.LoadLevel4();
                    break;
                default:
                    break;
            }
        }

        public void OnMenuButton()
        {
            level.LoadMainMenu();
        }
    }
}
