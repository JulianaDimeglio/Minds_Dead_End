using UnityEngine;
using UnityEngine.Playables;

public class MargaretGrabController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform screamerSpawnPoint;
    [SerializeField] private Transform targetLookAtMargaret;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private PlayerInputBlocker inputBlocker;
    [SerializeField] private PlayableDirector deathTimeline;
    [SerializeField] private GameObject activeMargaret;
    [SerializeField] private GameObject screamMargaret;

    private bool isGrabbing = false;

    public void TriggerGrab()
    {
        inputBlocker.BlockAll(); 

        isGrabbing = true;
    }

    private void Update()
    {
        if (!isGrabbing) return;

        Vector3 direction = (targetLookAtMargaret.position - playerCamera.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        playerCamera.rotation = Quaternion.RotateTowards(playerCamera.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 flatDirection = new Vector3(direction.x, 0, direction.z);
        if (flatDirection != Vector3.zero)
        {
            Quaternion playerTargetRotation = Quaternion.LookRotation(flatDirection);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, playerTargetRotation, rotationSpeed * Time.deltaTime); ;
        }

        float angle = Quaternion.Angle(playerCamera.rotation, targetRotation);
        if (angle < 5f)
        {
            screamMargaret.SetActive(true);
            PositionScreamMargaretInFront();
            isGrabbing = false;
            if (activeMargaret != null) activeMargaret.SetActive(false);
            deathTimeline.gameObject.SetActive(true);
            deathTimeline.Play();
        }
    }

    private void PositionScreamMargaretInFront()
    {
        if (screamMargaret == null || screamerSpawnPoint == null) return;

        screamMargaret.transform.position = screamerSpawnPoint.position;
        screamMargaret.transform.rotation = screamerSpawnPoint.rotation;
    }

    public void DisableMargaret()
    {

        screamMargaret.SetActive(false);

    }
}