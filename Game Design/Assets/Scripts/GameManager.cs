using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputManager _inputManager;
    private LevelManager _levelManager;

    void Start()
    {
        _levelManager.LoadLevel0();
    }

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _levelManager = GetComponent<LevelManager>();
    }
}
