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
            Rigidbody playerrb = other.GetComponent<Rigidbody>();

            if (playerrb!=null)
            {
                Vector3 originalVelocity = playerrb.velocity;
                Vector3 originalAngularVelocity = playerrb.angularVelocity;
                Vector3 localOffset = transform.InverseTransformPoint(other.transform.position);
                Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation);
                playerrb.isKinematic = true;
                other.transform.position = TeleportZoneObject.TransformPoint(localOffset);
                other.transform.rotation = relativeRotation * other.transform.rotation;
                playerrb.isKinematic = false;
                playerrb.velocity = relativeVelocity(relativeRotation, originalVelocity);
                playerrb.angularVelocity = relativeAngularVelocity(relativeRotation, originalAngularVelocity);
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
