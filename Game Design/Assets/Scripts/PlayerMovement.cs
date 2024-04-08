using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            lastDirection = movement.normalized; // Update lastDirection when the player moves
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public Vector2 GetFacingDirection()
    {
        return lastDirection;
    }
}
