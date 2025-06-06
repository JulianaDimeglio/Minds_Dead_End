using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ForcedFocusManager : MonoBehaviour
{
    public static ForcedFocusManager Instance { get; private set; }

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float focusSpeed = 2f;
    [SerializeField] private Image listening_icon;

    private bool isFocusing = false;
    private bool isOnTrigger = false;

    public bool IsFocusing => isFocusing;
    public void SetIsOnTrigger(bool isInsideTrigger)
    {
        isOnTrigger = isInsideTrigger;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void FocusOn(ILookableInteractable interactable)
    {
        if (isFocusing || !isOnTrigger) return;
        StartCoroutine(FocusRoutine(interactable));
    }

    private IEnumerator FocusRoutine(ILookableInteractable interactable)
    {
        isFocusing = true;
        PlayerStateManager.Instance?.SetState(PlayerState.Focus);

        Transform focusTarget = interactable.GetFocusTarget();
        Quaternion initialRotation = cameraTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(focusTarget.position - cameraTransform.position);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * focusSpeed;
            cameraTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        listening_icon.enabled = true;
        yield return interactable.PlayInteraction(() =>
        {
            PlayerStateManager.Instance?.SetState(PlayerState.Normal);
            isFocusing = false;
            listening_icon.enabled = false;
        });
    }

    public void FocusOn(ILookableInteractable interactable, Action onComplete)
    {
        if (isFocusing || !isOnTrigger) return;
        StartCoroutine(FocusRoutine(interactable, onComplete));
    }

    private IEnumerator FocusRoutine(ILookableInteractable interactable, Action onComplete)
    {
        isFocusing = true;
        PlayerStateManager.Instance?.SetState(PlayerState.Focus);

        Transform focusTarget = interactable.GetFocusTarget();
        Quaternion initialRotation = cameraTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(focusTarget.position - cameraTransform.position);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * focusSpeed;
            cameraTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        listening_icon.enabled = true;
        yield return interactable.PlayInteraction(() =>
        {
            PlayerStateManager.Instance?.SetState(PlayerState.Normal);
            isFocusing = false;
            listening_icon.enabled = false;
            onComplete?.Invoke(); // <- Aquí se llama correctamente
        });
    }
}