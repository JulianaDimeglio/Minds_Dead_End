using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float _interactDistance = 3f;
    [SerializeField] private LayerMask _interactLayer;

    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _interactDistance, Color.green);
        PlayerInteract();
    }

    private void PlayerInteract()
    {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactDistance, _interactLayer))
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
