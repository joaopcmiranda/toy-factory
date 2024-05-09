using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using machines;
using managers;
using items;

public class Level4_minigame : MonoBehaviour
{
    private Item item;
    //FIND A WAY TO FIND THIS PROGRAMMATICALLY
    public Level4_Machines machine;
    //public MachineManager machineManager;

    //getplayerdistance

    public bool gameEnabled = false;

    //private AudioManager audioManager;

    private void Start()
    {
        //machine = FindObjectOfType<Level4_Machines>();
        GameVisibility(false);
    }

    public void StartGame()
    {
        Debug.Log("Starting Minigame!!!");
        gameEnabled = true;

    }

    public void StopGame()
    {
        Debug.Log("Stopping Minigame!!!");
        gameEnabled = false;

    }

    private void Update()
    {
        //gameEnabled if within a distance
        if (gameEnabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                machine.TransformItem(machine.getItem()); 
                Debug.Log("Chop!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == item)
        {
            //clickable = true;
            Debug.Log("Enter2D");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == item)
        {
            //clickable = false;
            Debug.Log("Exit2D");
        }
    }

    private void EndGame()
    {
        GameVisibility(false);
        gameEnabled = false;
    }

    private void BreakItem()
    {
        EndGame();
        machine.BreakItems();
        //audioManager.PlayBreakItem();
    }

    private void GameVisibility(bool isGameVisible)
    {
        if(isGameVisible)
        {
            Debug.Log("Game Visible!");
        }
    }

}
