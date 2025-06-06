using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip toggleSound;
    [SerializeField] private string flashlightItemId = "flashlight";

    private bool isFlashlightOn = false;
    private bool isFlashlightBeingHaunted = false;

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

    public void ToggleFlashLight()
    {
        if (InventoryManager.Instance.HasItem(flashlightItemId))
        {
            if (flashlight != null)
            {
                PlayToggleSound();
                if (isFlashlightBeingHaunted)
                {
                    // If the flashlight is being haunted, do not toggle it
                    return;
                }
                isFlashlightOn = !isFlashlightOn;
                flashlight.enabled = isFlashlightOn;
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