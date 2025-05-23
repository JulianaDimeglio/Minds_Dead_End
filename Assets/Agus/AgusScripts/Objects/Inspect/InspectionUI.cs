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
        Hide(); // Por defecto, la UI está oculta
    }

    /// <summary>
    /// Muestra la descripción del objeto y, si es recolectable, el botón de "guardar".
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