// PlayerLook.cs
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float verticalClamp = 80f;

    private Transform _playerBody;
    [SerializeField] Transform _cameraTransform;
    private float _xRotation = 0f;

    private void Awake()
    {
        _playerBody = this.transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateView();
    }

    private void RotateView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // vertical rotation
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -verticalClamp, verticalClamp);
        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Rotación horizontal (cuerpo del jugador)
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}