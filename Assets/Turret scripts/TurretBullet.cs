using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float speed = 12f;       // How fast the bullet flies forward
    public float lifetime = 3f;     // Cleans up missed bullets to save memory
    public float hitRadius = 0.8f;  // Math hitbox size (raise this to 1.5 in the inspector if it still misses!)

    private Rigidbody ballRb;
    private Ball ballScript;

    void Start()
    {
        // 1. Set up automatic garbage collection if the bullet misses
        Destroy(gameObject, lifetime);
        
        // 2. Automatically find the ball in your game scene using its tag
        GameObject ballObj = GameObject.FindWithTag("Ball");
        if (ballObj != null)
        {
            ballRb = ballObj.GetComponent<Rigidbody>();
            ballScript = ballObj.GetComponent<Ball>();
        }
    }

    void Update()
    {
        // 3. Move the bullet forward manually through coordinate space
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 4. THE FIX: Constantly check the mathematical distance between bullet and ball
        if (ballRb != null && ballScript != null)
        {
            float distance = Vector3.Distance(transform.position, ballRb.transform.position);

            // If the coordinates overlap within the hit radius threshold
            if (distance <= hitRadius)
            {
                // Grab the ball's current tracking speed so it stays fast
                float currentSpeed = ballRb.linearVelocity.magnitude;
                if (currentSpeed < 1f) currentSpeed = ballScript.speed;

                // Forcefully redirect the ball's velocity vector along the bullet's exact trajectory
                ballRb.linearVelocity = transform.up * currentSpeed;

                // Play the ball's wall hit audio effect manually
                if (ballScript.audioSource != null && ballScript.wallHitSound != null)
                {
                    ballScript.audioSource.PlayOneShot(ballScript.wallHitSound);
                }

                // Vaporize the bullet instantly
                Destroy(gameObject);
            }
        }
    }
}
