using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using machines;
using managers;
using items;
using UnityEngine.UIElements;

public class Level4_minigame : MonoBehaviour
{
    public Level4_Machines machine;
    public MachineManager machineManager;

    public bool gameEnabled = false;
    public bool gameStarted = false; 

    public AudioManager audioManager;

    public virtual void Start()
    {
        GameVisibility(false);
    }

    public bool IsGameObjectInRange(Level4_Machines searchMachine)
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

    public virtual void StartGame()
    {
        //Debug.Log("Starting Minigame!!!");
        gameStarted = true;
    }

    public virtual void StopGame()
    {
        //Debug.Log("Stopping Minigame!!!");
        gameStarted = false;

    }

    public virtual void Update()
    {
        if (gameStarted)
        {
            if (IsGameObjectInRange(machine))
            {
                //Debug.Log("In range");
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

    public virtual void minigame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            machine.TransformItem(machine.getItem());
            Debug.Log("Chop!");
            machine.requiredFulfilled = false;
        } 
    }

    public virtual void EndGame()
    {
        GameVisibility(false);
        gameEnabled = false;
    }

    public virtual void GameVisibility(bool isGameVisible)
    {
        //Debug.Log("Game: " + isGameVisible + ".");
    }
}