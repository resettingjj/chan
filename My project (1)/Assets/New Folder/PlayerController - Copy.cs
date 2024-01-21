using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movemantValue)
    {
        Vector2 movemantVector = movemantValue.Get<Vector2>();

        movementX = movemantVector.x;
        movementY = movemantVector.y;
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        rb.AddForce(movement * speed);
    }
}
