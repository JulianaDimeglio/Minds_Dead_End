using UnityEngine;
using UnityEngine.XR;

public class MouseLookHandler : ILookHandler
{
    private readonly Transform _camera;
    private readonly Transform _hand;

    private float _cameraPitch = 0f;
    private Vector2 _currentMouseDelta;
    private Vector2 _mouseDeltaVelocity;
    private float _mouseSmoothTime = 0.03f;
    private float _sensitivity = 3.5f;
    private float _handRotationSpeed = 8f;

    public MouseLookHandler(Transform camera, Transform hand)
    {
        _camera = camera;
        _hand = hand;
    }

    public void UpdateLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _mouseDeltaVelocity, _mouseSmoothTime);

        _cameraPitch -= _currentMouseDelta.y * _sensitivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f);

        _camera.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        _hand.localRotation = Quaternion.Lerp(_hand.localRotation, Quaternion.Euler(_cameraPitch, 0f, 0f), _handRotationSpeed);

        _camera.parent.Rotate(Vector3.up * _currentMouseDelta.x * _sensitivity);
    }
}
