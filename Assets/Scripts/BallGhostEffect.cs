
using UnityEngine;
using System.Collections;

public class CubeAfterimage : MonoBehaviour
{
    public float spawnInterval = 0.02f;
    public float fadeDuration = 0.35f;
    public float minSpeed = 0f;

    private Rigidbody rb;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb == null || rb.linearVelocity.magnitude < minSpeed)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnAfterimage();
        }
    }

    void SpawnAfterimage()
    {
        GameObject ghost = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ghost.transform.position = transform.position;
        ghost.transform.rotation = transform.rotation;
        ghost.transform.localScale = transform.lossyScale;

        Destroy(ghost.GetComponent<Collider>());

        var renderer = ghost.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
        renderer.material.color = new Color(0.4f, 0.85f, 1f, 0.35f);

        StartCoroutine(Fade(ghost, renderer));
    }

    IEnumerator Fade(GameObject ghost, Renderer renderer)
    {
        float elapsed = 0f;
        Color start = renderer.material.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            Color c = start;
            c.a = Mathf.Lerp(start.a, 0f, elapsed / fadeDuration);
            renderer.material.color = c;
            yield return null;
        }

        Destroy(ghost);
    }
}
