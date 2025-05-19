using UnityEngine;

public class HeadbobHandler : IHeadbobHandler
{
    private readonly Transform _camera;
    private Vector3 _initialPos;
    private float _bobTimer;
    private float _breathTimer;

    private float _bobFrequency = 1.8f;
    private float _bobHorizontalAmplitude = 0.05f;
    private float _bobVerticalAmplitude = 0.025f;
    private float _bobTransitionSpeed = 6f;

    private float _breathFrequency = 0.8f;
    private float _breathVerticalAmplitude = 0.01f;
    private float _breathTiltAmplitude = 0.3f;

    public HeadbobHandler(Transform camera)
    {
        _camera = camera;
        _initialPos = camera.localPosition;
    }

    public void UpdateHeadbob(bool isMoving, bool isGrounded)
    {
        if (isMoving && isGrounded)
        {
            _bobTimer += Time.deltaTime * _bobFrequency;

            float horizontal = Mathf.Cos(_bobTimer) * _bobHorizontalAmplitude;
            float vertical = Mathf.Sin(_bobTimer * 2f);
            vertical = vertical < 0 ? -vertical * vertical : Mathf.Sqrt(vertical);
            vertical *= _bobVerticalAmplitude;

            Vector3 target = _initialPos + new Vector3(horizontal, vertical, 0);
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, target, Time.deltaTime * _bobTransitionSpeed);
        }
        else
        {
            _breathTimer += Time.deltaTime * _breathFrequency;
            float y = Mathf.Sin(_breathTimer) * _breathVerticalAmplitude;
            float tilt = Mathf.Sin(_breathTimer * 0.8f) * _breathTiltAmplitude;

            Vector3 target = _initialPos + new Vector3(0f, y, 0f);
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, target, Time.deltaTime * _bobTransitionSpeed);
            _camera.localRotation = Quaternion.Slerp(_camera.localRotation, Quaternion.Euler(0, 0, tilt), Time.deltaTime * _bobTransitionSpeed);
        }
    }
}
