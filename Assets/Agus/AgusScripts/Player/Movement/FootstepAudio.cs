using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(FPCameraEffects))]
public class FootstepAudio : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private List<AudioClip> footstepClips;
    [SerializeField] private float tiltThreshold = 0.1f;

    private FPCameraEffects cameraEffects;
    private bool hasStepped = false;

    private void Start()
    {
        cameraEffects = GetComponent<FPCameraEffects>();
    }

    private void Update()
    {
        float currentTilt = cameraEffects.GetCurrentTiltZ();

        if (Mathf.Abs(currentTilt) <= tiltThreshold && !hasStepped)
        {
            PlayFootstepSound();
            hasStepped = true;
        }
        else if (Mathf.Abs(currentTilt) > tiltThreshold)
        {
            hasStepped = false;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepClips.Count == 0 || footstepSource == null) return;

        int index = Random.Range(0, footstepClips.Count);
        footstepSource.PlayOneShot(footstepClips[index]);
    }
}