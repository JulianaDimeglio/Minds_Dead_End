using TMPro;
using UnityEngine;

public class ButtonClickController : MonoBehaviour, IInteraction
{
    private TextMeshPro textMeshPro;
    private int currentNumber = 0;
    public int buttonIndex;
    public ButtonsManager buttonsManager;
    public bool isInteracting = false;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        if (textMeshPro != null)
        {
            textMeshPro.text = currentNumber.ToString();
        }
        else
        {
            Debug.LogError("No se encontró el componente TextMeshPro en el cubo.");
        }
    }

    private void Update()
    {
        if (isInteracting) {
            OnButtonClick();
            Debug.Log("interacted");
            isInteracting =false;
        }
    }

    public void OnButtonClick()
    {
        currentNumber = (currentNumber + 1) % 10;

        textMeshPro.text = currentNumber.ToString();
        if (buttonsManager != null)
        {
            buttonsManager.UpdateList(buttonIndex, currentNumber);
        }
    }

    public void TriggerInteraction()
    {
        isInteracting = true;
        Debug.Log("interacted true");

    }
}
