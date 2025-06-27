using UnityEngine;
using System.Collections;

public class LightningManager : MonoBehaviour
{
    [Header("Luz del rayo")]
    [SerializeField] private Light flashLight;
    [SerializeField] private AudioSource thunderSound;

    [Header("Luz (baja tensión)")]
    [SerializeField] private Light objectToDim;
    [SerializeField] private float dimmedIntensity = 0.2f;
    private float originalDimIntensity;

    [Header("Luz con flicker constante")]
    [SerializeField] private Light flickerLight;
    [SerializeField] private Renderer lampRenderer;
    [SerializeField] private float minFlickerIntensity = 0.2f;
    [SerializeField] private float maxFlickerIntensity = 1.5f;
    [SerializeField] private float flickerSpeed = 0.05f;

    [Header("Intervalo entre rayos")]
    [SerializeField] private float minDelay = 5f;
    [SerializeField] private float maxDelay = 20f;

    [Header("Flash del rayo")]
    [SerializeField] private float fadeInTime = 0.2f;
    [SerializeField] private float visibleTime = 0.3f;
    [SerializeField] private float fadeOutTime = 0.5f;
    [SerializeField] private float maxFlashIntensity = 4f;

    private void Start()
    {
        if (flashLight == null)
            flashLight = GetComponent<Light>();

        if (objectToDim != null)
            originalDimIntensity = objectToDim.intensity;

        StartCoroutine(FlickerLoop());
        StartCoroutine(LightningLoop());
    }

    private IEnumerator FlickerLoop()
    {
        while (true)
        {
            if (flickerLight != null)
            {
                float intensity = Random.Range(minFlickerIntensity, maxFlickerIntensity);
                flickerLight.intensity = intensity;

                if (lampRenderer != null)
                    lampRenderer.enabled = intensity > 0.05f;

                yield return new WaitForSeconds(Random.Range(flickerSpeed / 2f, flickerSpeed * 2f));
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator LightningLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (thunderSound != null)
                thunderSound.Play();

            if (objectToDim != null)
                StartCoroutine(FadeLight(objectToDim, originalDimIntensity, dimmedIntensity, fadeInTime));

            yield return StartCoroutine(FadeLight(flashLight, flashLight.intensity, maxFlashIntensity, fadeInTime));
            yield return new WaitForSeconds(visibleTime);
            yield return StartCoroutine(FadeLight(flashLight, maxFlashIntensity, 0f, fadeOutTime));

            if (objectToDim != null)
                StartCoroutine(FadeLight(objectToDim, dimmedIntensity, originalDimIntensity, fadeOutTime));
        }
    }

    private IEnumerator FadeLight(Light light, float from, float to, float duration)
    {
        if (light == null) yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float intensity = Mathf.Lerp(from, to, elapsed / duration);
            light.intensity = intensity;

            yield return null;
        }

        light.intensity = to;
    }
}