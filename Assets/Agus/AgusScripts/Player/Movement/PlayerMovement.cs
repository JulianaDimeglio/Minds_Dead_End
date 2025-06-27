using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] Animator animator;

    private float originalSpeed;
    private CharacterController controller;
    private float velocityY;
    private bool isGrounded;
    private Vector2 currentDir;
    private Vector2 currentDirVelocity;

    public Vector2 MoveDirection => currentDir;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float OriginalSpeed
    {
        get => originalSpeed;
        set => originalSpeed = value;
    }

    private void Start()
    {
        originalSpeed = speed;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (PlayerInputBlocker.Instance != null && PlayerInputBlocker.Instance.BlockMovement)
        {
            currentDir = Vector2.zero;
            UpdateAnimator(0, 0);
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, inputDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * Time.deltaTime;

        Vector3 move = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed;
        Vector3 velocity = move + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (!isGrounded && controller.velocity.y < -1f)
            velocityY = -8f;

        UpdateAnimator(currentDir.x, currentDir.y);
    }

    private void UpdateAnimator(float x, float z)
    {
        if (animator != null)
        {
            animator.SetFloat("XMove", x);
            animator.SetFloat("ZMove", z);
        }
    }
}