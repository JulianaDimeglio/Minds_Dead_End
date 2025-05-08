using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public List<int> buttonsList = new List<int>();
    public List<int> correctPassword = new List<int> { 1, 2, 3, 4, 5, 6 };

    private float timer = 0f;
    private bool isTimerRunning = false;
    public float timeToCheck = 2f;

    void Start()
    {
        for (int i = 0; i < correctPassword.Count; i++)
        {
            buttonsList.Add(0);
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= timeToCheck)
            {
                isTimerRunning = false;
                timer = 0f;
                CheckPassword();
            }
        }
    }

    public void UpdateList(int i, int number)
    {
        if (i >= 0 && i < buttonsList.Count)
        {
            buttonsList[i] = number;
            StartTimer();
        }
        else
        {
            Debug.LogError($"Índice {i} fuera de rango");
        }
    }

    private void StartTimer()
    {
        timer = 0f;
        isTimerRunning = true;
    }

    public void CheckPassword()
    {
        for (int i = 0; i < correctPassword.Count; i++)
        {
            if (buttonsList[i] != correctPassword[i])
            {
                Debug.Log("Contraseña incorrecta");
                return;
            }
        }
        Debug.Log("Contraseña correcta");
    }
}
