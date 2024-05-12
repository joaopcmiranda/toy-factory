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
    public MachineManager machineManager;

    //getplayerdistance

    public bool gameEnabled = false;
    public bool gameStarted = false; 

    //private AudioManager audioManager;

    private void Start()
    {

        //machine = FindObjectOfType<Level4_Machines>();
        GameVisibility(false);
    }

    bool IsGameObjectInRange(Level4_Machines searchMachine)
    {
        foreach (var tuple in machineManager.getMachinesInRadius())
        {
            if (tuple.Item2 == searchMachine)
            {
                return true;
            }
        }
        return false;
    }

    public void StartGame()
    {
        Debug.Log("Starting Minigame!!!");
        gameStarted = true;

    }

    public void StopGame()
    {
        Debug.Log("Stopping Minigame!!!");
        gameStarted = false;

    }

    private void Update()
    {
        if (gameStarted)
        {
            if (IsGameObjectInRange(machine))
            {
                gameEnabled = true;
            }
            else
            {
                gameEnabled = false;
            }
            if (gameEnabled)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    machine.TransformItem(machine.getItem());
                    Debug.Log("Chop!");
                    machine.requiredFulfilled = false;
                }
            }
        } 
        else
        {
            gameEnabled = false; 
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
