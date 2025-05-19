using UnityEngine;

/// <summary>
/// Makes the object rotate only on the Y axis to face the player (main camera).
/// </summary>
public class FaceCamera : MonoBehaviour
{
    [Tooltip("Optional Y-axis rotation offset (e.g., 180 if facing backwards).")]
    [SerializeField] private float yRotationOffset = 0f;

    private Transform _cameraTransform;

    private void Start()
    {
        // Cache the main camera's transform for performance
        if (Camera.main != null)
        {
            _cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("[FaceCamera] No MainCamera found. Make sure your camera has the 'MainCamera' tag.");
        }
    }

    private void Update()
    {
        if (_cameraTransform == null) return;

        Vector3 direction = _cameraTransform.position - transform.position;
        direction.y = 0f; // Lock rotation to horizontal plane

        if (direction.sqrMagnitude < 0.001f) return; // Avoid zero direction

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y + yRotationOffset, 0f);
    }
}