using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float smoothTime = 0.05f; // Suavidad del movimiento.

    private float xRotation = 0f;
    private Vector2 currentMouse;
    private Vector2 currentMouseSpeed;

    public Rigidbody playerRigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180) xRotation -= 360;
    }

    void LateUpdate()
    {
        // Movimiento del mouse crudo
        float rawMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float rawMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Vector2 rawMouseDelta = new Vector2(rawMouseX, rawMouseY);

        // Suavizado de movimiento. Da "peso" al movimiento.
        currentMouse = Vector2.SmoothDamp(currentMouse, rawMouseDelta, ref currentMouseSpeed, smoothTime);

        // Rotación vertical (cámara)
        xRotation -= currentMouse.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 60f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (cuerpo del jugador)
        Quaternion deltaRotation = Quaternion.Euler(0f, currentMouse.x, 0f);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
    }
}

