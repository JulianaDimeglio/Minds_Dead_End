using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectionUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
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

        // Activar desenfoque
        BlurManager blur = FindObjectOfType<BlurManager>();
        if (blur != null)
            blur.ShowInspection();

        descriptionText.text = description;
        nameText.text = displayName;
        storeText.enabled = canBeCollected;
    }

    public void Hide()
    {
        panel.SetActive(false);

        // Desactivar desenfoque
        BlurManager blur = FindObjectOfType<BlurManager>();
        if (blur != null)
            blur.HideInspection();
    }
}