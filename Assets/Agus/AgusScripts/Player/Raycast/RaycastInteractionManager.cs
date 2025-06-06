using Game.Puzzles;
using UnityEngine;

public class RaycastInteractionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxDistance = 2.5f;
    [SerializeField] private LayerMask interactionMask;
    public void TryInteract()
    {
        if (UIStateManager.Instance.IsAnyUIOpen || InspectionManager.Instance.IsInspecting)
            return;


        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactionMask))
        {
            GameObject hitObj = hit.collider.gameObject;
            Debug.Log($"hit: {hitObj.name}");
            if (hitObj.TryGetComponent<ILookableInteractable>(out var lookable))
            {
                ForcedFocusManager.Instance.FocusOn(lookable);
            }
            else if (hitObj.TryGetComponent<InspectableItem>(out var inspectable))
            {
                InspectionManager.Instance.StartInspect(inspectable);
            }
            else if (hitObj.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
            else if (hitObj.TryGetComponent<LightSwitch>(out var lightSwitch))
            {
                lightSwitch.Toggle();
            }
            else if (hitObj.TryGetComponent<PhotoFramePuzzle>(out var puzzle))
            {
                puzzle.Activate();
            }
            else if (hitObj.TryGetComponent<Door>(out var door))
            {
                door.Toggle();
            }
            else if (hitObj.TryGetComponent<SimpleOpenClose>(out var simpleOpenClose))
            {
                hitObj.BroadcastMessage("ObjectClicked");
            }
        }
    }
}