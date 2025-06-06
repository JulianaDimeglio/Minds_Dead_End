using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

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
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        if (targetDir.magnitude > 0.01f)
            currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * (1f / moveSmoothTime));
        else
            currentDir = Vector2.zero;

        controller.Move(velocity * Time.deltaTime);

        if (!isGrounded && controller.velocity.y < -1f)
            velocityY = -8f;
    }
}