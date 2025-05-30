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
        Hide(); // Por defecto, la UI está oculta
    }

    /// <summary>
    /// Muestra la descripción del objeto y, si es recolectable, el botón de "guardar".
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