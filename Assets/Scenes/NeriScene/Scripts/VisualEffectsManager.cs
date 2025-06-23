using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class VisualEffectsManager : MonoBehaviour
{
    [SerializeField] private PostProcessVolume volume;

    [Header("Max Effect Intensities")]
    [SerializeField] private float maxAberration = 1.0f;
    [SerializeField] private float maxGrain = 0.6f;
    [SerializeField] private float maxVignette = 0.4f;
    [SerializeField] private float maxDistortion = 40f;
    [SerializeField] private float fadeOutDuration = 1.5f;

    private ChromaticAberration _aberration;
    private Grain _grain;
    private Vignette _vignette;
    private LensDistortion _distortion;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (volume == null)
        {
            Debug.LogWarning("[VisualEffectsManager] PostProcessVolume not assigned.");
            return;
        }

        // Get post-processing effect settings from the volume
        volume.profile.TryGetSettings(out _aberration);
        volume.profile.TryGetSettings(out _grain);
        volume.profile.TryGetSettings(out _vignette);
        volume.profile.TryGetSettings(out _distortion);
    }

    // Called when the player is looking at the enemy
    public void ApplyEffects(float t)
    {
        t = Mathf.Clamp01(t);

        // Stop fading if player is looking again
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (_aberration != null) _aberration.intensity.value = Mathf.Lerp(0f, maxAberration, t);
        if (_grain != null) _grain.intensity.value = Mathf.Lerp(0f, maxGrain, t);
        if (_vignette != null) _vignette.intensity.value = Mathf.Lerp(0f, maxVignette, t);
        if (_distortion != null) _distortion.intensity.value = Mathf.Lerp(0f, maxDistortion, t);
    }

    // Called when the player stops looking
    public void ResetAll()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutEffects());
    }

    // Smoothly reduce all effects over time
    private IEnumerator FadeOutEffects()
    {
        float elapsed = 0f;

        float startAberration = _aberration != null ? _aberration.intensity.value : 0f;
        float startGrain = _grain != null ? _grain.intensity.value : 0f;
        float startVignette = _vignette != null ? _vignette.intensity.value : 0f;
        float startDistortion = _distortion != null ? _distortion.intensity.value : 0f;

        while (elapsed < fadeOutDuration)
        {
            float t = 1f - (elapsed / fadeOutDuration);

            if (_aberration != null) _aberration.intensity.value = startAberration * t;
            if (_grain != null) _grain.intensity.value = startGrain * t;
            if (_vignette != null) _vignette.intensity.value = startVignette * t;
            if (_distortion != null) _distortion.intensity.value = startDistortion * t;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Fully disable all effects at the end
        if (_aberration != null) _aberration.intensity.value = 0f;
        if (_grain != null) _grain.intensity.value = 0f;
        if (_vignette != null) _vignette.intensity.value = 0f;
        if (_distortion != null) _distortion.intensity.value = 0f;
    }
}