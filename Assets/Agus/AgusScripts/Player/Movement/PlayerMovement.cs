// PlayerMovement.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController _controller;
    private IPlayerInput _input;

    private Vector3 _velocity;
    [SerializeField] Transform _camTransform;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<IPlayerInput>();

        if (_input == null)
        {
            Debug.LogError("[PlayerMovement] IPlayerInput not found.");
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 input = _input.GetMovementInput();

        // Convert input to world direction based on camera orientation
        Vector3 move = _camTransform.right * input.x + _camTransform.forward * input.y;
        move.y = 0f; // Prevent moving up/down

        _controller.Move(move * walkSpeed * Time.deltaTime);

        // Apply gravity
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Keep grounded
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}