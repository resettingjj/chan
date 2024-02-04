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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, 75f);
    }
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        Vector2 c = (Vector2)this.transform.position + (movement * speed);
        if (transform.position.magnitude > 35)
        {
            rb.AddForce(-transform.position * speed);
        }
        else
        {
            rb.AddForce(movement * speed);
        }
    }
}
