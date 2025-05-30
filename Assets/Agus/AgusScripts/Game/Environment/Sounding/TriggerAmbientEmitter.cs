using UnityEngine;

public class TriggerAmbientEmitter : MonoBehaviour
{
    [SerializeField] private AmbientSoundEmitter emitter;
    [SerializeField] private bool activateOnEnter = true;
    [SerializeField] private bool deactivateOnExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (activateOnEnter)
            emitter.ActivateEmitter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (deactivateOnExit)
            emitter.DeactivateEmitter();
    }
}