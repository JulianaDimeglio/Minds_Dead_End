using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactDistance, Color.green);
        PlayerInteract();
    }

    private void PlayerInteract()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            var detectable = hit.collider.GetComponent<IDetectableByPlayer>();
            if (detectable != null)
            {
                detectable.OnSeenByPlayer();
                Debug.Log("[RaycastPlayer] Detected: " + detectable);
            }
        }
    }
}
