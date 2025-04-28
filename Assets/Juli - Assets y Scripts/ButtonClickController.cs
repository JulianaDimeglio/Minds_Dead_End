using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonClickController : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    private int currentNumber = 0;
    public int buttonIndex;
    public ButtonsManager buttonsManager;

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

    public void OnButtonClick()
    {
        currentNumber = (currentNumber + 1) % 10;

        textMeshPro.text = currentNumber.ToString();
        if (buttonsManager != null)
        {
            buttonsManager.UpdateList(buttonIndex, currentNumber);
        }
    }
}
