using UnityEngine;

public class LookableTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                ForcedFocusManager.Instance.SetIsOnTrigger(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ForcedFocusManager.Instance.SetIsOnTrigger(false);
        }
    }
}