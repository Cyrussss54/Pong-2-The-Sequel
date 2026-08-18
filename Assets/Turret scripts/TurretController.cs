using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform targetBall;     
    public GameObject bulletPrefab;  
    public Transform firePoint;      
    
    public float fireRate = 2f;      
    private float nextFireTime;

    void Update()
    {
        if (targetBall == null) return;

        // Tracks and rotates the turret toward the ball on the 2D plane
        Vector3 direction = targetBall.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); 

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
