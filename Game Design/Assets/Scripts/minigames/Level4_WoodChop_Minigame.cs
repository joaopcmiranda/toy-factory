using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : Level4_minigame
{
    public TextMeshProUGUI instructionText;
    public Image progressBar;
    public GameObject releaseIndicator;

    public float holdTime = 3f; // Time in seconds to hold the key
    public float releaseTime = 2f; // Time in seconds to release the key
    public int requiredSuccesses = 3; // Number of successful completions required

    public bool holdingKey = false;
    public float holdTimer = 0f;
    public int successes = 0;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void minigame()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            machine.TransformItem(machine.getItem());
            Debug.Log("Chop Wood!");
            machine.requiredFulfilled = false;
        }
    }

    public override void EndGame()
    {
        GameVisibility(false);
        gameEnabled = false;
    }
    public override void GameVisibility(bool isGameVisible)
    {
        //Debug.Log("Game: " + isGameVisible + ".");
    }

}
