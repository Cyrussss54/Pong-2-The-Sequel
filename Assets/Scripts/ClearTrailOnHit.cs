
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ClearTrailOnHit : MonoBehaviour
{
    private TrailRenderer trail;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trail.Clear();
        }
    }
}
