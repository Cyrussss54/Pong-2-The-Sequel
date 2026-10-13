using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float speed = 12f;      
    public float lifetime = 3f;     
    public float hitRadius = 0.8f;  

    private Rigidbody ballRb;
    private Ball ballScript;

    void Start()
    {
        Destroy(gameObject, lifetime);
        
        GameObject ballObj = GameObject.FindWithTag("Ball");
        if (ballObj != null)
        {
            ballRb = ballObj.GetComponent<Rigidbody>();
            ballScript = ballObj.GetComponent<Ball>();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (ballRb != null && ballScript != null)
        {
            float distance = Vector3.Distance(transform.position, ballRb.transform.position);

            if (distance <= hitRadius)
            {
                float currentSpeed = ballRb.linearVelocity.magnitude;
                if (currentSpeed < 1f) currentSpeed = ballScript.speed;

                ballRb.linearVelocity = transform.up * currentSpeed;

                if (ballScript.audioSource != null && ballScript.wallHitSound != null)
                {
                    ballScript.audioSource.PlayOneShot(ballScript.wallHitSound);
                }

                Destroy(gameObject);
            }
        }
    }
}
