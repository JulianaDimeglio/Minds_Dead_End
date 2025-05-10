using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public List<int> buttonsList = new List<int>();
    public GameObject buttonsLight;
    //position of each note corresponding to a position in password 
    private List<int> posNoteInPassword = new List<int>();
    [SerializeField]
    public TextMeshPro hintText;



    [Header("Passwords:")]
    public List<int> correctPassword;

    [Header("Notes:")]
    public List<NoteController> notes;

    //time variables to delay password check 
    private float timer = 0f;
    private bool isTimerRunning = false;
    public float timeToCheck = 2f;

    void Start()
    {
        RandomizePassword();
        ChargeEmptyButtons();
        UpdateNotes();
    }

    void Update()
    {
        //if the player interacted check after the delay
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

    //initialize the players button input list with default zeros
    private void ChargeEmptyButtons()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            buttonsList.Add(0);
        }
        ChangeColorFeedback(Color.white);
    }

    //called when a button is pressed, updates the value and restarts the timer
    public void UpdateList(int i, int number)
    {
        if (i >= 0 && i < notes.Count)
        {
            buttonsList[i] = number;
            StartTimer();
        }
    }

    private void StartTimer()
    {
        timer = 0f;
        isTimerRunning = true;
    }



    // Check if the players input matches the password
    public void CheckPassword()
    {
        for (int i = 0; i < correctPassword.Count; i++)
        {
            if (buttonsList[i] != correctPassword[i])
            {
                ChangeColorFeedback(Color.red);
                UpdateHint();
                UpdateNotes();
                return;
            }
        }
        ChangeColorFeedback(Color.green);
    }
    //generates a random password with numbers from 0 to 9
    private void RandomizePassword()
    {
        correctPassword.Clear();
        for (int i = 0; i < notes.Count; i++)
        {
            correctPassword.Add(Random.Range(0, 10));
        }

        ChargeEmptyButtons();
    }
    //changes the emission color of the feedback light
    void ChangeColorFeedback(Color color)
    {
        var mat = buttonsLight.GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color);
    }

    private void UpdateNotes()
    {
        posNoteInPassword.Clear();

        //auxiliar variable to assign the pasword positions randomly 
        List<int> passwordPositions = new List<int>();
        for (int i = 0; i < correctPassword.Count; i++)
            passwordPositions.Add(i);

        for (int i = 0; i < notes.Count; i++)
        {
            int randomIndex = Random.Range(0, passwordPositions.Count);
            int passwordPos = passwordPositions[randomIndex];

            int value = correctPassword[passwordPos];
            //shows the value 
            notes[i].UpdateNumber(value);
            //store the positions
            posNoteInPassword.Add(passwordPos);
            //avoid repeting 
            passwordPositions.RemoveAt(randomIndex);
        }

        UpdateHint();
    }

    //shows the current current position in the password of the notes
    public void UpdateHint()
    {
        string aux = "";

        for (int i = 0; i < notes.Count; i++)
        {
            int noteValue = notes[i].GetCurrentNumber();
            int passwordPos = posNoteInPassword[i];
            aux += $"Note {i + 1} -> {passwordPos + 1}\n";
        }

        hintText.text = aux;
    }





}