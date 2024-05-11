using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using machines;
using managers;
using items;

public class Level4_minigame : MonoBehaviour
{
    private Item item;

    public Level4_Machines machine;
    public MachineManager machineManager;

    public bool gameEnabled = false;
    public bool gameStarted = false; 

    public AudioManager audioManager;

    private void Start()
    {
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
                GameVisibility(true);
                gameEnabled = true;
            }
            else
            {
                GameVisibility(false);
                gameEnabled = false;
            }
            if (gameEnabled)
            {
                minigame();    
            }
        } 
        else
        {
            GameVisibility(false);
            gameEnabled = false; 
        }      
    }

    private void minigame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            machine.TransformItem(machine.getItem());
            Debug.Log("Chop!");
            machine.requiredFulfilled = false;
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
        audioManager.PlayBreakItem();
    }

    private void GameVisibility(bool isGameVisible)
    {
        Debug.Log("Game " + isGameVisible + ".");
    }
}
