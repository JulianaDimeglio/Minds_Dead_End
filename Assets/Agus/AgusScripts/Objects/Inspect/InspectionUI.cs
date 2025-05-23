using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectionUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject panel; // El panel principal
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText; // Nombre del objeto
    [SerializeField] private TextMeshProUGUI storeText;

    private void Awake()
    {
        Hide(); // Por defecto, la UI est� oculta
    }

    /// <summary>
    /// Muestra la descripci�n del objeto y, si es recolectable, el bot�n de "guardar".
    /// </summary>
    public void Show(string description, string displayName, bool canBeCollected)
    {

        panel.SetActive(true);
        descriptionText.text = description;
        nameText.text = displayName;
        if (canBeCollected)
        {
            storeText.enabled = true;
        }
        else
        {
            storeText.enabled = false;
        }
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}