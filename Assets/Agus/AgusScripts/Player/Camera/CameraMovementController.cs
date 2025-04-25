/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform hand;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField][Range(0.0f, 5f)] float mouseSmoothTime = 0.181f;
    [SerializeField] float handRotationSpeed = 8f;
    [SerializeField] bool cursorLock = true;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    Vector3 cameraInitialPos;
    float cameraCap;


    // Start is called before the first frame update
    void Start()
    {
        cameraInitialPos = playerCamera.localPosition;
        cameraCap = playerCamera.localEulerAngles.x;
        if (cameraCap > 180f) cameraCap -= 360f;
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouse();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);


        Quaternion cameraTargetRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        playerCamera.localRotation = cameraTargetRotation;

        Quaternion handTargetRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        hand.localRotation = Quaternion.Lerp(hand.localRotation, handTargetRotation, Time.deltaTime * handRotationSpeed);

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
}
*/