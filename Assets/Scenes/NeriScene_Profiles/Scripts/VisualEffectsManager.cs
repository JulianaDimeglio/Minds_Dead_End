using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisualEffectsManager : MonoBehaviour
{
    [Header("Post Processing")]
    [SerializeField] private PostProcessVolume postProcessVolume;

    private ChromaticAberration chromaticAberration;

    [Header("Fade Settings")]
    [SerializeField] private float fadeOutSpeed = 0.5f; // Qué tan rápido baja cuando no miras
    [SerializeField] private float maxAberration = 1f;  // Valor máximo permitido

    private bool isIncreasing = false; // Para saber si lo están mirando

    private void Awake()
    {
        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out chromaticAberration))
        {
            chromaticAberration.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("[VisualEffectsManager] Chromatic Aberration not found in profile.");
        }
    }

    private void Update()
    {
        if (chromaticAberration != null)
        {
            // Si no se está sumando (no lo están mirando), hacemos el fade out
            if (!isIncreasing && chromaticAberration.intensity.value > 0f)
            {
                chromaticAberration.intensity.value -= fadeOutSpeed * Time.deltaTime;
                if (chromaticAberration.intensity.value < 0f)
                    chromaticAberration.intensity.value = 0f;
            }

            // Reset el flag cada frame. Solo suma si alguien llama IncreaseAberration ese frame.
            isIncreasing = false;
        }
    }

    /// <summary>
    /// Increases the chromatic aberration intensity while the player is looking at the enemy.
    /// </summary>
    public void IncreaseAberration(float amount)
    {
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.Clamp(chromaticAberration.intensity.value + amount * Time.deltaTime, 0f, maxAberration);
            isIncreasing = true;
        }
    }

    /// <summary>
    /// No longer forces reset. Fade out is handled in Update.
    /// </summary>
    public void ResetAberration()
    {
        // Vacío porque ahora el Update maneja el fade out automáticamente
    }
}
