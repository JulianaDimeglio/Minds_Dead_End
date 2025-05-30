using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmbientSoundEvent", menuName = "Audio/Ambient Sound Event")]
public class AmbientSoundEvent : ScriptableObject
{
    public string eventName;
    public List<AudioClip> clips;

    [Header("Timing")]
    public float minInterval = 2f;
    public float maxInterval = 6f;

    [Header("Pitch & Volume")]
    [Range(0.5f, 2f)] public float pitchMin = 0.95f;
    [Range(0.5f, 2f)] public float pitchMax = 1.05f;
    [Range(0f, 1f)] public float volumeMin = 0.8f;
    [Range(0f, 1f)] public float volumeMax = 1f;

    [Header("Optional Position Settings")]
    public bool overrideYPosition = false;
    public float fixedY = 0.1f; // altura del suelo por defecto
}