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
        //gets the text component in children and show the starting number (0)
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        if (textMeshPro != null)
        {
            textMeshPro.text = currentNumber.ToString();
        }
    }

    private void Update()
    {
        if (isInteracting)
        {
            OnButtonClick();
            isInteracting = false;
        }
    }

    public void OnButtonClick()
    {
        //updates current number
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
    }
}