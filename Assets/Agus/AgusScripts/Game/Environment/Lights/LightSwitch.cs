using UnityEngine;
using Game.Environment.Lights;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class LightSwitch : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private List<HauntedLight> linkedLights;
    [SerializeField] private bool startOn = false;

    [Header("Audio")]
    [SerializeField] private AudioClip switchOnSound;
    [SerializeField] private AudioClip switchOffSound;
    [SerializeField] private AudioClip blockedSound;

    [SerializeField]private Animator _animator;

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

        // Animación de palanca
        if (_animator != null)
        {
            _animator.ResetTrigger("On");
            _animator.ResetTrigger("Off");
            _animator.SetTrigger(_isOn ? "On" : "Off");
        }

        ApplyCurrentState();
    }

    /// <summary>
    /// Attempts to apply the current desired state to the light.
    /// If the light is blocked (e.g., no power, broken, paranormal event), plays a blocked sound.
    /// </summary>
    private void ApplyCurrentState()
    {
        bool atLeastOneSuccess = false;
        bool atLeastOneBlocked = false;

        for (int i = 0; i < linkedLights.Count; i++)
        {

            HauntedLight light = linkedLights[i];
            if (light == null) continue;
            bool executed = light.ApplySwitchState(_isOn);
            if (executed)
                atLeastOneSuccess = true;
            else
                atLeastOneBlocked = true;
        }

        if (atLeastOneBlocked && blockedSound != null)
            _audioSource.PlayOneShot(blockedSound);
        else if (atLeastOneSuccess)
            _audioSource.PlayOneShot(_isOn ? switchOnSound : switchOffSound);
    }

    /// <summary>
    /// Forces the light off (used by environmental events).
    /// </summary>
    public void ForceOff()
    {
        for(int i = 0; i < linkedLights.Count; i++)
        {
            HauntedLight linkedLight = linkedLights[i];
            linkedLight.TurnOff();
        }
    }

    public void ForceOn()
    {
        foreach (var light in linkedLights)
            light.TurnOn();
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