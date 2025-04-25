using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages ambient audio sources such as radios, speakers, and environmental music.
/// </summary>
public class AmbientAudioManager : MonoBehaviour
{
    [Tooltip("List of controllable ambient audio sources in the scene.")]
    [SerializeField] private List<AmbientAudioSource> audioSources = new();

    private Dictionary<string, AmbientAudioSource> _audioMap = new();

    private void Awake()
    {
        foreach (var audio in audioSources)
        {
            if (audio != null && !_audioMap.ContainsKey(audio.Id))
                _audioMap[audio.Id] = audio;
        }
    }

    public void SetRadioState(string id, bool on)
    {
        if (_audioMap.TryGetValue(id, out var source))
            source.SetState(on);
        else
            Debug.LogWarning($"[AmbientAudioManager] Radio with ID '{id}' not found.");
    }

    public void Play(string id)
    {
        if (_audioMap.TryGetValue(id, out var source))
            source.Play();
        else
            Debug.LogWarning($"[AmbientAudioManager] Ambient audio '{id}' not found.");
    }

    public void Stop(string id)
    {
        if (_audioMap.TryGetValue(id, out var source))
            source.Stop();
        else
            Debug.LogWarning($"[AmbientAudioManager] Ambient audio '{id}' not found.");
    }
}