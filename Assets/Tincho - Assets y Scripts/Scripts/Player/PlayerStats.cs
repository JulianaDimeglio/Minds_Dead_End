using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _currentStamina;
    [SerializeField] private float _staminaUseRate;
    [SerializeField] private float _staminaRegenRate;
    [SerializeField] private float _timerBase = 10f;
    [SerializeField] private float _timerDelay;
    [SerializeField] private float _timerEarlyRecoverBase = 3;
    [SerializeField] private float _timerEarlyRecover;
    [SerializeField] public bool staminaIsBeingConsumed;

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

    // Early Delay se activa cuando:
    // - La stamina no esta al 100%
    // - La stamina no esta al 0%
    // - La stamina no esta siendo consumida.

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


// Timer corto sigue descontando una vez timer largo llega a su fin.
// Timer corto descuenta despues de que el timer largo termina hasta que la stamina llega a su maximo.