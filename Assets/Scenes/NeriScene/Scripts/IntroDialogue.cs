using System.Collections;
using UnityEngine;
using TMPro;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float[] waitTimes; 
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float fadeDuration = 0.5f;

    private int currentLine = 0;

    void Start()
    {
        dialogueText.text = "";
        StartCoroutine(RunDialogue());
    }

    private IEnumerator RunDialogue()
    {
        while (currentLine < dialogueLines.Length)
        {
            // Fade Out 
            yield return StartCoroutine(FadeText(0f));

            // change text
            dialogueText.text = dialogueLines[currentLine];

            // Fade In
            yield return StartCoroutine(FadeText(1f));

            // time to wait
            float waitTime = (currentLine < waitTimes.Length) ? waitTimes[currentLine] : 2f; // fallback
            yield return new WaitForSeconds(waitTime);

            currentLine++;
        }

        // finish dialogue
        yield return StartCoroutine(FadeText(0f));
        dialogueText.text = "";
    }

    private IEnumerator FadeText(float targetAlpha)
    {
        float startAlpha = dialogueText.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            dialogueText.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        dialogueText.alpha = targetAlpha;
    }
}