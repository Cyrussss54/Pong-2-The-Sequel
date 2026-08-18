using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public Vector3 startPosition;
    public float speedMultiplier = 1.05f;

    public AudioSource audioSource;
    public AudioClip paddleHitSound;
    public AudioClip wallHitSound;
    public AudioClip goalSound;

    private float paddleCooldown = 0f;
    private float wallCooldown = 0f;

    void Start()
    {
        startPosition = transform.position;
        Launch();
    }

    public void Reset()
    {
        if (audioSource != null && goalSound != null)
        {
            audioSource.PlayOneShot(goalSound);
        }

        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(speed * x, speed * y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 1. PADDLE COLLISIONS
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time < paddleCooldown) return;
            paddleCooldown = Time.time + 0.1f;

            float currentSpeed = rb.linearVelocity.magnitude;
            if (currentSpeed < speed) currentSpeed = speed;
            currentSpeed *= speedMultiplier;

            float directionX = (rb.linearVelocity.x > 0) ? -1 : 1;
            float directionY = (rb.linearVelocity.y > 0) ? 1 : -1;
            float randomYModifier = Random.Range(0.8f, 1.2f);

            Vector3 bouncedDirection = new Vector3(directionX, directionY * randomYModifier, 0).normalized;
            rb.linearVelocity = bouncedDirection * currentSpeed;

            if (audioSource != null && paddleHitSound != null)
            {
                audioSource.PlayOneShot(paddleHitSound);
            }
        }

        // 2. WALL COLLISIONS
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (Time.time < wallCooldown) return;
            wallCooldown = Time.time + 0.1f;

            float currentSpeed = rb.linearVelocity.magnitude;
            if (currentSpeed < speed) currentSpeed = speed;

            float directionX = (rb.linearVelocity.x > 0) ? 1 : -1;
            float directionY = (rb.linearVelocity.y > 0) ? -1 : 1;

            Vector3 bouncedDirection = new Vector3(directionX, directionY, 0).normalized;
            rb.linearVelocity = bouncedDirection * currentSpeed;

            if (audioSource != null && wallHitSound != null)
            {
                audioSource.PlayOneShot(wallHitSound);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("THE SAFETY NET WAS TOUCHED BY: " + other.gameObject.name);

        if (other.CompareTag("Safety"))
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = startPosition;
            Launch();
        }
        else if (other.CompareTag("Bullet"))
        {
            Debug.Log("BULLET DEFLECTED SUCCESSFULLY!");

            float currentSpeed = rb.linearVelocity.magnitude;
            if (currentSpeed < speed) currentSpeed = speed;

            Vector3 pushDirection = other.transform.up;
            rb.linearVelocity = pushDirection * currentSpeed;

            if (audioSource != null && wallHitSound != null)
            {
                audioSource.PlayOneShot(wallHitSound);
            }

            Destroy(other.gameObject);
        }
    }
}
