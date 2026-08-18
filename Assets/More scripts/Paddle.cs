using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody rb;
    public Vector3 startPosition;

    private float movement;

    void Start()
    {
        startPosition = transform.position;
    }
    
    // Update is called once per frame

    // Drag your Ball GameObject into this slot in the Unity Inspector for Paddle 2!
    public Transform ball; 

    void Update()
    {
        if (isPlayer1)
        {
            // Player 1 controls (Arrow keys or W/S)
            movement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            // AI Logic for Paddle 2: Track the ball's height
            if (ball != null)
            {
                // If the ball is higher than the paddle, move UP (1)
                if (ball.position.y > transform.position.y + 0.2f)
                {
                    movement = 1f;
                }
                // If the ball is lower than the paddle, move DOWN (-1)
                else if (ball.position.y < transform.position.y - 0.2f)
                {
                    movement = -1f;
                }
                else
                {
                    movement = 0f; // Stay still if perfectly aligned
                }
            }
        }
    }

   
    void FixedUpdate()
{
    if (rb != null)
    {
        float nextY = rb.position.y + (movement * speed * Time.fixedDeltaTime);
        nextY = Mathf.Clamp(nextY, -4f, 6f); // Change these numbers to fit your walls!
        rb.MovePosition(new Vector3(rb.position.x, nextY, rb.position.z));
    }
}



    public void Reset()
    {
        transform.position = startPosition;
    }
}
