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

        public void OnStartButton()
        {
            level.LoadLevel0();
        }

        public void OnOptionsButton() {}

        public void OnExitButton() 
        {
            //it doesn't work in unity, but probaly works in the real game
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
