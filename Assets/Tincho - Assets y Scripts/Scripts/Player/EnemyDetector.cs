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
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {
                Debug.Log("Player is interacting with " + hit.collider.name);

                IInteraction interactable = hit.collider.GetComponent<IInteraction>();
                if (interactable != null)
                {
                    interactable.TriggerInteraction();
                }
            }
    }
}
