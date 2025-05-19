public interface IMovementHandler
{
    void UpdateMovement();
    bool IsMoving { get; }
    bool IsGrounded { get; }
}