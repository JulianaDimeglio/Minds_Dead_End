using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;
    private float staminaUseRate;
    private float staminaRegenRate;

    void Start()
    {
        currentStamina = maxStamina;
    }

    public bool CanSprint()
    {
        return currentStamina > 0;
    }

    public void UseStamina()
    {
        currentStamina -= staminaUseRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    public void RecoverStamina()
    {
        currentStamina += staminaRegenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }
}
