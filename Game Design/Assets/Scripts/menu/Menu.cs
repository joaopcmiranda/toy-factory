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
        private int levelScene; //level that the player is or have played in

        void Start()
        {
            level = GameObject.Find("Managers").GetComponent<LevelManager>();
        }

        void Update() {}

        public void OnStartButton()
        {
            level.LoadLevel0();
            levelScene = 0;
        }

        public void OnOptionsButton() { }

        public void OnExitButton() 
        {
            Application.Quit(); 
        }

        public void OnInstantWinButton()
        {
            level.LoadLevelEnd();
        }

        public void OnNextLevelButton()
        { 
            switch (levelScene)
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
                    level.LoadMainMenu(); //note: make a end game scene, not end level scene
                    break;
            }
        }

        public void OnMenuButton()
        {
            level.LoadMainMenu();
        }
    }
}
