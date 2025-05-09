using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // This class handles how stamina is consumed and its limits.

    [Header("Stamina")]
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _currentStamina;
    [SerializeField] private float _staminaUseRate;
    [SerializeField] private float _staminaRegenRate;
    [SerializeField] private float _timerBase = 10f;
    [SerializeField] private float _timerDelay;
    [SerializeField] private float _timerEarlyRecoverBase = 3;
    [SerializeField] private float _timerEarlyRecover;
    public bool staminaIsBeingConsumed;
    public bool canSprint = true;
    public Image staminaBar;

    void Start()
    {
        _currentStamina = _maxStamina;
        _timerDelay = _timerBase;

        staminaIsBeingConsumed = false;
    }

    private void Update()
    {
        StaminaDelay();
        RestartDelay();
        ManageStaminaBar();
        EarlyDelay();
    }

    public void UseStamina()
    {
        _currentStamina -= _staminaUseRate * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
    }

    //Early Delay activated when:
    // - Stamina is not at 100%.
    // - Stamina is not at 0%.
    // - Stamina is not being consumed.

    public void EarlyDelay()
    {
        if (!staminaIsBeingConsumed && _currentStamina > 0)
        {
            _timerEarlyRecover -= Time.deltaTime;
        }
        else
        {
            _timerEarlyRecover = _timerEarlyRecoverBase;
        }
    }

    public void StaminaDelay()
    {
        if (_currentStamina <= 0)
        {
            _timerDelay -= Time.deltaTime;
            canSprint = false;
        }
    }
    
    public void RecoverStaminaFromZero()
    {
        if (_timerDelay <= 0 && _currentStamina <= 0)
        {
            _currentStamina += _staminaRegenRate * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
        }
        
    }

    public void RecoverIncompletedStamina()
    {
        if (_timerEarlyRecover <= 0)
        {
            _currentStamina += _staminaRegenRate * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
        }

    }

    public void RestartDelay()
    {
        if (_currentStamina >= _maxStamina)
        {
            _timerDelay = _timerBase;
            canSprint = true;
            
        }
    }

    public void ManageStaminaBar()
    {
        staminaBar.fillAmount = _currentStamina / _maxStamina;
    }
}
