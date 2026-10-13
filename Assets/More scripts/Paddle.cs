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
    public GameObject ball; 
    public float aiDeadzone = 0.2f; 

    void Update()
    {
        float moveInput = 0f;

        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveInput = 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;
        }
        else
        {
            if (ball != null)
            {
                float verticalDistance = ball.transform.position.y - transform.position.y;

                if (verticalDistance > aiDeadzone)
                {
                    moveInput = 1f;
                }
                else if (verticalDistance < -aiDeadzone)
                {
                    moveInput = -1f;
                }
            }
        }

        transform.Translate(Vector3.up * moveInput * speed * Time.deltaTime);

        float clampedY = Mathf.Clamp(transform.position.y, minYLimit, maxYLimit);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
    
    public void Reset()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
