using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Player Assignment")]
    public bool isPlayer1 = true; 

    [Header("Movement Settings")]
    public float speed = 10f;
    public float maxYLimit = 4.5f;   
    public float minYLimit = -4.5f;  

    [Header("AI Tracking Settings (Right Paddle Only)")]
    public GameObject ball; // 🛠️ LINK THIS SLOT FOR PADDLE 2 IN THE INSPECTOR!
    public float aiDeadzone = 0.2f; // Helps stop the AI from violently jittering up and down

    void Update()
    {
        float moveInput = 0f;

        if (isPlayer1)
        {
            // 🎮 PLAYER 1 CONTROLS (Left Paddle)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveInput = 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;
        }
        else
        {
            // 🤖 AI TRACKING CONTROLS (Right Paddle)
            if (ball != null)
            {
                // Calculate the exact height difference between the ball and this AI paddle
                float verticalDistance = ball.transform.position.y - transform.position.y;

                // Move up if the ball is above the paddle outside the deadzone buffer
                if (verticalDistance > aiDeadzone)
                {
                    moveInput = 1f;
                }
                // Move down if the ball is below the paddle outside the deadzone buffer
                else if (verticalDistance < -aiDeadzone)
                {
                    moveInput = -1f;
                }
            }
        }

        // Apply movement translation smoothly over time
        transform.Translate(Vector3.up * moveInput * speed * Time.deltaTime);

        // Enforce your separate top and bottom boundary limitations safely
        float clampedY = Mathf.Clamp(transform.position.y, minYLimit, maxYLimit);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
    
    public void Reset()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
