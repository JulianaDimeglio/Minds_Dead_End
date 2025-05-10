using UnityEngine;

public class Flashlight : MonoBehaviour
{
    //This class handles how the flashlight works: by pressing E the flashlight turns on and pressing E again, the flashlight will turn off.
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
                //print("Flashligth turned on.");
            }
            else if ((_isFlashLightOn && Input.GetKeyDown(KeyCode.E)))
            {
                _flashlight.enabled = false;
                _isFlashLightOn = false;
                //print("Flashligth turned on.");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _flashlightSFX.Play();
        }
        
    }
}
