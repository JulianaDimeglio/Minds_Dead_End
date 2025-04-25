using UnityEngine;
using System.Collections;
using Game.Environment.Lights;

public enum LightGroup
{
    Kitchen, Bathroom, Bedroom, LivingRoom, Hallway, Lamp, Other
}

public enum LightState
{
    On, Off, Broken
}

[RequireComponent(typeof(Light), typeof(AudioSource))]
public class HauntedLight : MonoBehaviour
{
    [Header("Identificación")]
    public LightGroup group;
    public string lightId;

    [Header("Flicker Settings")]
    public float minInterval = 0.02f;
    public float maxInterval = 0.3f;
    public float minOffDuration = 0.01f;
    public float maxOffDuration = 0.2f;

    [Header("Low Voltage Settings")]
    public bool allowVoltageDrop = true;
    [Range(0f, 1f)] public float lowVoltageIntensity = 0.3f;
    public float voltageDropChance = 0.1f;
    public float minDropDuration = 0.5f;
    public float maxDropDuration = 2f;

    [Header("Break Settings")]
    public bool canBreak = true;
    public float breakChancePerFlick = 0.02f;
    public int maxFlicksBeforeBreak = 50;
    public AudioClip breakSound;

    [Header("Audio")]
    public AudioClip flickerSound;

    private Light _light;
    private AudioSource _audioSource;

    private Coroutine _flickerRoutine;
    private Coroutine _externalFlashRoutine;

    private LightState _currentState = LightState.Off;
    private LightState _previousStateBeforePowerCut = LightState.Off;

    private float _originalIntensity;
    private int _flickCount = 0;
    private bool _isFlickering = false;
    private bool _isLocked = false; // evita control manual externo

    private void Awake()
    {
        _light = GetComponent<Light>();
        _audioSource = GetComponent<AudioSource>();
        _originalIntensity = _light.intensity;

        TurnOff();
    }

    public void SetLocked(bool locked)
    {
        _isLocked = locked;
    }

    public void TurnOn()
    {
        if (_currentState == LightState.Broken || _isLocked || !LightingManager.Instance.IsPowerOn) return;
        Debug.Log(_light);
        _light.enabled = true;
        _light.intensity = _originalIntensity;
        _currentState = LightState.On;
    }

    public void TurnOff()
    {
        if (_currentState == LightState.Broken || _isLocked) return;

        _light.enabled = false;
        _currentState = LightState.Off;
    }

    public void StartFlicker()
    {
        //Debug.Log($"light {name}, isFlickering: {_isFlickering}, currentState: _currentState, isLocked: {_isLocked}, PowerOn: {LightingManager.Instance.IsPowerOn}");
        if (_isFlickering || _currentState == LightState.Broken || _isLocked || !LightingManager.Instance.IsPowerOn)
            return;

        _flickCount = 0;
        _isFlickering = true;
        _flickerRoutine = StartCoroutine(FlickerLoop());
    }

    public void StopFlicker()
    {
        if (!_isFlickering) return;

        _isFlickering = false;

        if (_flickerRoutine != null)
            StopCoroutine(_flickerRoutine);

        if (_externalFlashRoutine != null)
        {
            StopCoroutine(_externalFlashRoutine);
            _externalFlashRoutine = null;
        }

        if (_currentState != LightState.Broken)
        {
            _light.intensity = _originalIntensity;
            _light.enabled = (_currentState == LightState.On);
        }
    }

    private IEnumerator FlickerLoop()
    {
        while (_isFlickering)
        {
            _flickCount++;

            //if (canBreak && (_flickCount >= maxFlicksBeforeBreak || Random.value < breakChancePerFlick))
            //{
            //    BreakLight();
            //    yield break;
            //}

            _light.enabled = false;
            PlaySound(flickerSound);
            yield return new WaitForSeconds(Random.Range(minOffDuration, maxOffDuration));

            _light.enabled = true;
            PlaySound(flickerSound);

            if (allowVoltageDrop && Random.value < voltageDropChance)
            {
                float dropTime = Random.Range(minDropDuration, maxDropDuration);
                _light.intensity = lowVoltageIntensity;
                yield return new WaitForSeconds(dropTime);
                _light.intensity = _originalIntensity;
            }

            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }

    /// <summary>
    /// Attempts to apply a desired state from a switch.
    /// Returns true if the state was applied, false if blocked (e.g. no power, broken).
    /// </summary>
    public bool ApplySwitchState(bool shouldBeOn)
    {
        if (IsBroken || !LightingManager.Instance.IsPowerOn || _isLocked)
            return false;

        if (shouldBeOn) TurnOn();
        else TurnOff();

        return true;
    }

    private void BreakLight()
    {
        _isFlickering = false;
        _currentState = LightState.Broken;

        if (_flickerRoutine != null)
            StopCoroutine(_flickerRoutine);

        if (_externalFlashRoutine != null)
            StopCoroutine(_externalFlashRoutine);

        _light.enabled = false;

        if (breakSound != null)
            _audioSource.PlayOneShot(breakSound);

        Debug.Log($"[HauntedLight] '{name}' se rompió permanentemente.");
    }

    public void OnPowerCut()
    {
        _previousStateBeforePowerCut = _currentState;

        if (_flickerRoutine != null)
            StopCoroutine(_flickerRoutine);

        _light.enabled = false;
    }

    public void OnPowerRestored()
    {
        if (_currentState == LightState.Broken) return;

        if (_previousStateBeforePowerCut == LightState.On) TurnOn();
        else TurnOff();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
            _audioSource.PlayOneShot(clip);
    }

    // Método externo: forzar que parpadee X veces con intervalo de 1 segundo
    public void FlashPattern(int count, float interval = 1f)
    {
        if (_externalFlashRoutine != null)
            StopCoroutine(_externalFlashRoutine);

        _externalFlashRoutine = StartCoroutine(FlashRoutine(count, interval));
    }

    private IEnumerator FlashRoutine(int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            if (_currentState == LightState.Broken || !LightingManager.Instance.IsPowerOn)
                yield break;

            _light.enabled = true;
            yield return new WaitForSeconds(interval);
            _light.enabled = false;
            yield return new WaitForSeconds(interval);
        }
    }

    public bool IsBroken => _currentState == LightState.Broken;
    public string LightId => lightId;
    public LightGroup Group => group;
}
