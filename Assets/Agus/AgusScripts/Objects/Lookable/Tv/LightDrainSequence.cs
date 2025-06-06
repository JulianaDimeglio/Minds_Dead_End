using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Environment.Lights;
using UnityEngine.Video;

public class LightDrainSequence : MonoBehaviour
{
    [SerializeField] private List<HauntedLight> targetLights;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private TVVideoController tvVideoController;

    [Header("Timing")]
    [Tooltip("Video percentage after which the tension drop starts")]
    [Range(0f, 1f)] public float startDrainAt = 0.5f;

    [Tooltip("Video percentage after which the lights turn off")]
    [Range(0f, 1f)] public float finalOffAt = 0.9f;

    [Header("Drain Effect")]
    public float minIntensityMultiplier = 0.1f;
    public float drainSpeed = 0.5f;
    public float flickerProbability = 0.1f;
    public float flickerDuration = 0.1f;

    [Header("Sound")]
    public AudioClip drainStartSound;

    private Dictionary<HauntedLight, float> _baseIntensities = new();
    private Dictionary<HauntedLight, float> _currentIntensities = new();
    private HashSet<HauntedLight> _playedDrainSound = new();
    private Dictionary<HauntedLight, List<Color>> _currentEmissionColors = new();

    private bool _drainingStarted = false;
    private bool _lightsOff = false;

    private void Start()
    {
        foreach (var light in targetLights)
        {
            if (light != null)
            {
                float intensity = light.GetComponentInChildren<Light>().intensity;
                _baseIntensities[light] = intensity;
                _currentIntensities[light] = intensity;

                // Copiar colores actuales de emisión para modificar durante el drain
                var matList = new List<Color>();
                foreach (var mat in light.GetComponentsInChildren<Renderer>())
                {
                    foreach (var m in mat.materials)
                    {
                        if (m.HasProperty("_EmissionColor"))
                            matList.Add(m.GetColor("_EmissionColor"));
                    }
                }
                _currentEmissionColors[light] = matList;
            }
        }
    }

    public void StartDrainSequence()
    {
        if (!gameObject.activeInHierarchy) return;
        StopAllCoroutines();
        StartCoroutine(MonitorAndDrain());
    }

    private IEnumerator MonitorAndDrain()
    {
        while (videoPlayer != null && videoPlayer.clip != null && tvVideoController.IsMainVideoPlaying)
        {
            double progress = videoPlayer.time / videoPlayer.clip.length;

            // Fase 1 – Comienza la caída de tension
            if (!_drainingStarted && progress >= startDrainAt)
            {
                _drainingStarted = true;
                StartCoroutine(DrainRoutine());
            }

            // Fase 2 – Apagado total
            if (!_lightsOff && progress >= finalOffAt)
            {
                _lightsOff = true;
                foreach (var light in targetLights)
                {
                    if (light != null)
                    {
                        light.TurnOff();
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator DrainRoutine()
    {
        while (!_lightsOff)
        {
            foreach (var light in targetLights)
            {
                if (light == null || light.IsBroken) continue;

                Light l = light.GetComponentInChildren<Light>();
                if (l == null) continue;

                float target = _baseIntensities[light] * minIntensityMultiplier;
                float current = _currentIntensities[light];
                float newIntensity = Mathf.MoveTowards(current, target, Time.deltaTime * drainSpeed);

                _currentIntensities[light] = newIntensity;
                l.intensity = newIntensity;

                // Reproducir sonido de bajada de tensión (una vez por luz)
                if (!_playedDrainSound.Contains(light))
                {
                    AudioSource audioSource = light.GetComponent<AudioSource>();
                    if (audioSource != null && drainStartSound != null)
                        audioSource.PlayOneShot(drainStartSound);

                    _playedDrainSound.Add(light);
                }

                // Reducir emissive del material
                var renderers = light.GetComponentsInChildren<Renderer>();
                int colorIndex = 0;

                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        if (!mat.HasProperty("_EmissionColor")) continue;

                        // Obtener color base desde el diccionario
                        if (_currentEmissionColors.TryGetValue(light, out var baseColors) && colorIndex < baseColors.Count)
                        {
                            Color baseColor = baseColors[colorIndex];
                            Color drainedColor = Color.Lerp(baseColor, Color.black, 1f - (newIntensity / _baseIntensities[light]));
                            mat.SetColor("_EmissionColor", drainedColor);
                            mat.EnableKeyword("_EMISSION");
                        }

                        colorIndex++;
                    }
                }

                // Flicker ocasional no bloqueante
                if (Random.value < flickerProbability)
                    StartCoroutine(QuickFlicker(l));
            }

            yield return null;
        }
    }

    private IEnumerator QuickFlicker(Light l)
    {
        l.enabled = false;
        yield return new WaitForSeconds(flickerDuration);
        l.enabled = true;
    }
}