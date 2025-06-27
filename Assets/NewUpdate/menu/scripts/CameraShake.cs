using UnityEngine;

public class CreepyCameraShake : MonoBehaviour
{
    public float intensity = 0.05f;       // fuerza tambaleo
    public float frequency = 0.5f;        // frecuencia mov
    public float smoothness = 2f;         // smoth camera

    private Vector3 originalPosition;
    private float timer;

    void Start()
    {
        originalPosition = transform.localPosition;
        timer = Random.Range(0f, 100f); 
    }

    void Update()
    {
        timer += Time.deltaTime * frequency;

        float offsetX = Mathf.PerlinNoise(timer, 0.0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0.0f, timer) - 0.5f;

        Vector3 targetPosition = originalPosition + new Vector3(offsetX, offsetY, 0f) * intensity;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothness);
    }
}