using TMPro;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] private TextMeshPro numberText;
    //stores the current number value associated with the note 
    private int currentNumber;

    public void UpdateNumber(int number)
    {
        currentNumber = number;
        if (numberText != null)
        {
            numberText.text = number.ToString();
        }
    }

    public int GetCurrentNumber()
    {
        return currentNumber;
    }
}
