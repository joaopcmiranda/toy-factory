using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private Player _player;

    public void RegisterPlayer(Player player)
    {
        _player = player;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            _player.SetMovementDirection(new Vector2(horizontal, vertical));
        }

    }
}
