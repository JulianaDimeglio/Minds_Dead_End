using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip toggleSound;

    private void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        if (flashlight != null)
        {
            flashlight.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (flashlight != null)
            {
                flashlight.enabled = !flashlight.enabled;
                PlayToggleSound();
            }
        }
    }

    private void PlayToggleSound()
    {
        if (audioSource != null && toggleSound != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }
    }
}