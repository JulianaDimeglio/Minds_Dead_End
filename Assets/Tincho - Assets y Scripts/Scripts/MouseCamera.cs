using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float smoothTime = 0.05f; // Qué tan suave es el movimiento

    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180) xRotation -= 360;
    }

    void Update()
    {
        // Movimiento del mouse crudo
        float rawMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float rawMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Vector2 rawMouseDelta = new Vector2(rawMouseX, rawMouseY);

        // Aplicamos suavizado (esto le da ese "peso")
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, rawMouseDelta, ref currentMouseDeltaVelocity, smoothTime);

        // Rotación vertical (cámara)
        xRotation -= currentMouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 60f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (cuerpo del jugador)
        playerBody.Rotate(Vector3.up * currentMouseDelta.x);
    }
}
