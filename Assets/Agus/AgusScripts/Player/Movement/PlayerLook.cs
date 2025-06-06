using UnityEngine;
using UnityEngine.XR;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform hand;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float handRotationSpeed = 8f;
    [SerializeField][Range(0.0f, 5f)] float mouseSmoothTime = 0.03f;

    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;
    private float cameraCap;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        bool inputBlocked = PlayerInputBlocker.Instance != null && PlayerInputBlocker.Instance.BlockLook;
        if (!inputBlocked)
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            cameraCap -= currentMouseDelta.y * mouseSensitivity;
            cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);

            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
            // Esta línea se ejecuta siempre, pero con la rotación ya calculada
            playerCamera.localRotation = Quaternion.Euler(cameraCap, 0f, 0f);

            Quaternion handTargetRotation = Quaternion.Euler(cameraCap, 0f, 0f);
            hand.localRotation = Quaternion.Lerp(hand.localRotation, handTargetRotation, Time.deltaTime * handRotationSpeed);
        }

    }
}