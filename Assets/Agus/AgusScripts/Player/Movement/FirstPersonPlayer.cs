using UnityEngine;

public class FirstPersonPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private CharacterController _controller;
    private ILookHandler _lookHandler;
    private IMovementHandler _movementHandler;
    private IHeadbobHandler _headbobHandler;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _lookHandler = new MouseLookHandler(playerCamera, hand);
        _movementHandler = new SimpleMovementHandler(_controller, transform, groundCheck, groundMask);
        _headbobHandler = new HeadbobHandler(playerCamera);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _lookHandler.UpdateLook();
        _movementHandler.UpdateMovement();
        _headbobHandler.UpdateHeadbob(_movementHandler.IsMoving, _movementHandler.IsGrounded);
    }
}
