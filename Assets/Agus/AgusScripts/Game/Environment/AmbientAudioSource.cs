using UnityEngine;

/// <summary>
/// Represents a controllable ambient audio source in the scene.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AmbientAudioSource : MonoBehaviour
{
    [Tooltip("Unique ID to reference this audio source.")]
    public string Id;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sets the source to on/off like a radio.
    /// </summary>
    public void SetState(bool on)
    {
        if (on && !_audio.isPlaying)
            _audio.Play();
        else if (!on && _audio.isPlaying)
            _audio.Stop();
    }

    /// <summary>
    /// Plays the audio source once.
    /// </summary>
    public void Play()
    {
        if (!_audio.isPlaying)
            _audio.Play();
    }

    /// <summary>
    /// Stops the audio source.
    /// </summary>
    public void Stop()
    {
        if (_audio.isPlaying)
            _audio.Stop();
    }
}