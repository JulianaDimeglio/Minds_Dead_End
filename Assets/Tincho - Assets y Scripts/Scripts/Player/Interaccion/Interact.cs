using UnityEngine;

public class Interact : MonoBehaviour
{
    // This class handles how the player interacts with objects under the layer "Interactable" using Raycast and an Interface.

    [Header("Raycast Settings")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private AudioSource _interactSFX;

    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactDistance, Color.green);
        PlayerInteract();
    }

    // Player interacts with objects using F.
    private void PlayerInteract()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
              
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {
                _interactSFX.Play();
                Debug.Log("Player is interacting with " + hit.collider.name);

                IInteraction interactable = hit.collider.GetComponent<IInteraction>();
                if (interactable != null)
                {
                    interactable.TriggerInteraction();
                }
            }
        }
    }
}
