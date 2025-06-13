using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip ringClip;
    [SerializeField] private AudioClip hangupClip;
    [SerializeField] private List<AudioClip> ghostDialogues;
    [SerializeField] private AudioSource ringAudioSource;
    [SerializeField] private AudioSource callAudioSource;

    private bool isRinging = false;
    private bool shouldPlayDialogue = false;
    private Action onInteractionFinished;

    public void StartRinging(bool shouldPlayDialogue, Action onFinished)
    {
        this.shouldPlayDialogue = shouldPlayDialogue;
        this.onInteractionFinished = onFinished;
        isRinging = true;

        ringAudioSource.clip = shouldPlayDialogue ? ringClip : hangupClip;
        ringAudioSource.loop = shouldPlayDialogue; // solo hacer loop si es ringClip
        ringAudioSource.Play();

        if (!shouldPlayDialogue)
        {
            StartCoroutine(EndHangupSound());
        }
    }

    public void Interact()
    {
        if (!isRinging || !shouldPlayDialogue) return;

        ringAudioSource.Stop();
        isRinging = false;
        int nextIteration = LoopManager.Instance.CurrentIteration;
        if (nextIteration <= ghostDialogues.Count)
        {
            AudioClip dialogue = ghostDialogues[nextIteration];
            StartCoroutine(PlayDialogue(dialogue));
        }
        else
        {
            onInteractionFinished?.Invoke();
        }
    }

    private IEnumerator PlayDialogue(AudioClip clip)
    {
        callAudioSource.loop = false;
        callAudioSource.clip = clip;
        callAudioSource.Play();
        yield return new WaitForSeconds(clip.length);
        onInteractionFinished?.Invoke();
    }

    private IEnumerator EndHangupSound()
    {
        yield return new WaitForSeconds(hangupClip.length);
        isRinging = false;
        onInteractionFinished?.Invoke();
    }
}


