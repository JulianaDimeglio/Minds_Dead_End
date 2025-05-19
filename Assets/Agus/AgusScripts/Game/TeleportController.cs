using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform TeleportZoneObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
            {
                Vector3 localOffset = transform.InverseTransformPoint(other.transform.position);
                Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation);
                cc.enabled = false;
                other.transform.position = TeleportZoneObject.TransformPoint(localOffset);
                other.transform.rotation = relativeRotation * other.transform.rotation;
                cc.enabled = true;
            }
        }
    }

    private Vector3 relativeVelocity(Quaternion rotation, Vector3 velocity)
    {
        return rotation * velocity;
    }

    private Vector3 relativeAngularVelocity(Quaternion rotation, Vector3 angularVelocity)
    {
        return rotation * angularVelocity;
    }
}
