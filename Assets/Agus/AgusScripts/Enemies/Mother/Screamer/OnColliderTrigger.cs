using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColliderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if tag player touches collider, call TriggerGrab on MargaretGrabController
        if (Input.GetKeyDown(KeyCode.Space)) // Simulating a trigger for testing
        {
            MargaretGrabController grabController = FindObjectOfType<MargaretGrabController>();
            if (grabController != null)
            {
                grabController.TriggerGrab();
            }
        }
    }
}
