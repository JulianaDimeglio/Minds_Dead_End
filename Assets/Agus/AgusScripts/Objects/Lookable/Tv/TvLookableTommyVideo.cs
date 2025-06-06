using UnityEngine;
using System.Collections;

public class TVLookableTommyVideo : MonoBehaviour, ILookableInteractable
{
    [SerializeField] private Transform focusPoint;
    [SerializeField] private TVVideoController tvVideoController;
    [SerializeField] private TVController tvController;
    [SerializeField] private LightDrainSequence lightDrainSequence;
    [SerializeField] private GameObject margaret;

    public Transform GetFocusTarget() => focusPoint;

    public IEnumerator PlayInteraction(System.Action onComplete)
    {
        tvController.TurnOn();
        tvVideoController.PlayMainVideo(() =>
        {
            tvVideoController.PlayStaticVideo();
            if (margaret != null)
                margaret.SetActive(true);
            LoopManager.Instance.SetConditionMet(true);
        });

        lightDrainSequence.StartDrainSequence();

        while (tvVideoController.IsMainVideoPlaying)
            yield return null;

        onComplete?.Invoke();
        Destroy(this);
    }
}