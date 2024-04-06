using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 _movementDirection;
    public Vector2 _currentMovement;
    public float speed = 5.0f;
    public float damping = 0.1f;

    public void SetMovementDirection(Vector2 direction)
    {
        _movementDirection = direction.normalized;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Tag Manager -> component -> InputManager
       GameObject
           .FindWithTag("Manager")
           .GetComponent<InputManager>()
           .RegisterPlayer(this);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(_currentMovement.x, _currentMovement.y, 0) * (speed * Time.deltaTime);
        _currentMovement = Vector2.Lerp(_currentMovement, _movementDirection, damping);
    }
}
