using UnityEngine;
using System.Collections;

public class MargaretScareTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scareClip;
    [SerializeField] private GameObject visualModel;
    [SerializeField] private TVController tvController;

    private bool _hasBeenTriggered = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void TriggerScare()
    {
        if (_hasBeenTriggered) return;
        _hasBeenTriggered = true;

        if (audioSource != null && scareClip != null)
            audioSource.PlayOneShot(scareClip);

        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake(1.4f, 0.4f);

        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        yield return new WaitForSeconds(2f);

        if (tvController != null)
            tvController.TurnOff();

        if (visualModel != null)
            visualModel.SetActive(false);

        Destroy(gameObject, 1f);
    }
}