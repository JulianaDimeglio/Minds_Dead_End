using UnityEngine;
using Game.Environment.Lights;

[RequireComponent(typeof(AudioSource))]
public class LightSwitch : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private HauntedLight linkedLight;
    [SerializeField] private bool startOn = false;

    [Header("Audio")]
    [SerializeField] private AudioClip switchOnSound;
    [SerializeField] private AudioClip switchOffSound;
    [SerializeField] private AudioClip blockedSound;

    private AudioSource _audioSource;
    private bool _isOn;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _isOn = startOn;
    }

    private void Start()
    {
        ApplyCurrentState(); // Try to apply the initial state (on/off)
    }

    /// <summary>
    /// Called when the player interacts with the switch.
    /// Toggles the desired state.
    /// </summary>
    public void Toggle()
    {
        _isOn = !_isOn;
        ApplyCurrentState();
    }

    /// <summary>
    /// Attempts to apply the current desired state to the light.
    /// If the light is blocked (e.g., no power, broken, paranormal event), plays a blocked sound.
    /// </summary>
    private void ApplyCurrentState()
    {
        bool executed = linkedLight.ApplySwitchState(_isOn);

        if (!executed && blockedSound != null)
            _audioSource.PlayOneShot(blockedSound);
        else
            _audioSource.PlayOneShot(_isOn ? switchOnSound : switchOffSound);
    }

    /// <summary>
    /// Forces the light off (used by environmental events).
    /// </summary>
    public void ForceOff()
    {
        linkedLight.TurnOff();
    }

    /// <summary>
    /// Reapplies the last known switch state (used after interruptions).
    /// </summary>
    public void RestoreState()
    {
        ApplyCurrentState();
    }

    public bool IsOn => _isOn;
}