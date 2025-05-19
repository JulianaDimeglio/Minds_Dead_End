using UnityEngine;

public class SimpleMovementHandler : IMovementHandler
{
    private readonly CharacterController _controller;
    private readonly Transform _playerTransform;
    private readonly Transform _groundCheck;
    private readonly LayerMask _groundMask;

    private float _speed = 6f;
    private float _gravity = -30f;
    private float _moveSmoothTime = 0.3f;
    private float _velocityY;
    private Vector2 _currentDir;
    private Vector2 _currentDirVelocity;

    public bool IsMoving => _currentDir.magnitude > 0.1f;
    public bool IsGrounded { get; private set; }

    public SimpleMovementHandler(CharacterController controller, Transform playerTransform, Transform groundCheck, LayerMask groundMask)
    {
        _controller = controller;
        _playerTransform = playerTransform;
        _groundCheck = groundCheck;
        _groundMask = groundMask;
    }

    public void UpdateMovement()
    {
        IsGrounded = Physics.CheckSphere(_groundCheck.position, 0.4f, _groundMask);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Movimiento suave solo al empezar a moverse
        if (targetDir.magnitude > 0.01f)
        {
            _currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentDirVelocity, _moveSmoothTime);
        }
        else
        {
            _currentDir = Vector2.zero;
            _currentDirVelocity = Vector2.zero;
        }

        _velocityY += _gravity * Time.deltaTime;
        if (IsGrounded && _velocityY < 0)
            _velocityY = -2f;

        Vector3 velocity = (_playerTransform.forward * _currentDir.y + _playerTransform.right * _currentDir.x) * _speed;
        velocity.y = _velocityY;

        _controller.Move(velocity * Time.deltaTime);
    }
}