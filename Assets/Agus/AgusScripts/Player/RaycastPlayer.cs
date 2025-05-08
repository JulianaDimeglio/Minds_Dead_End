using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red);
        if (Physics.Raycast(ray, out hit, 50f))
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
