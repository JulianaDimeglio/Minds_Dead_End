using TMPro;
using UnityEngine;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayDuration = 3f;

    private Coroutine subtitleCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Opción A: duración fija (displayDuration)
    public void ShowSubtitle(string message)
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleCoroutine = StartCoroutine(FadeInOut(message, displayDuration));
    }

    // Opción B: duración personalizada
    public void ShowSubtitle(string message, float customDuration)
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleCoroutine = StartCoroutine(FadeInOut(message, customDuration));
    }

    private IEnumerator FadeInOut(string message, float duration)
    {
        subtitleText.text = message;

        Color color = subtitleText.color;
        color.a = 0f;
        subtitleText.color = color;

        // Fade in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            subtitleText.color = color;
            yield return null;
        }

        // Wait for duration
        yield return new WaitForSeconds(duration);

        // Fade out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            subtitleText.color = color;
            yield return null;
        }
    }
}