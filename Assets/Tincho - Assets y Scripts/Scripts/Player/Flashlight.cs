using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light _flashlight;
    private bool _isFlashLightOn;
    private AudioSource _flashlightSFX;

    void Start()
    {
        _flashlight = GetComponentInChildren<Light>();
        _flashlight.enabled = false;
        _isFlashLightOn = false;
        _flashlightSFX = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        TurnOnFlashlight();
    }

    private void TurnOnFlashlight()
    {
        if (_flashlight != null)
        {
            if (!_isFlashLightOn && Input.GetKeyDown(KeyCode.E))
            {
                _flashlight.enabled = true;
                _isFlashLightOn = true;
                //print("Linterna prendida.");
            }
            else if ((_isFlashLightOn && Input.GetKeyDown(KeyCode.E)))
            {
                _flashlight.enabled = false;
                _isFlashLightOn = false;
                //print("Linterna apagada.");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _flashlightSFX.Play();
        }
        
    }
}
