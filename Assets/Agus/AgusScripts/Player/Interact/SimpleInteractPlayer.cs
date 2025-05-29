using Game.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractPlayer : MonoBehaviour
{
    public GameObject mainCamera;
    private GameObject objectClicked;
    public GameObject flashlight;
    public KeyCode OpenClose = KeyCode.Mouse0;
    public KeyCode Flashlight = KeyCode.F;
    public LayerMask raycastMask;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(OpenClose) && UIStateManager.Instance.CurrentState == UIState.None) // Open and close action
        {
            RaycastCheck();
        }
    }

    void RaycastCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 2.3f, raycastMask))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<PhotoFramePuzzle>() && UIStateManager.Instance.CurrentState == UIState.None)
            {
                hit.collider.gameObject.GetComponent<PhotoFramePuzzle>().Activate();
            }
            else if (hit.collider.gameObject.GetComponent<InspectableItem>() && !InspectionManager.Instance.IsInspecting && UIStateManager.Instance.CurrentState == UIState.None)
            {
                InspectableItem item = hit.collider.GetComponent<InspectableItem>();
                InspectionManager.Instance.StartInspect(item);
            }
            else if (hit.collider.gameObject.GetComponent<Door>() && !InspectionManager.Instance.IsInspecting)
            {
                hit.collider.gameObject.GetComponent<Door>().Toggle();
            }
            else if (hit.collider.gameObject.GetComponent<SimpleOpenClose>() && !InspectionManager.Instance.IsInspecting)
            {
                hit.collider.gameObject.BroadcastMessage("ObjectClicked");
            }


        }
    }

}
