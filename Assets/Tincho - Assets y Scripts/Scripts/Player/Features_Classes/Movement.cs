using UnityEngine;

public class Movement
{
    // This class handles the movement in the player. It receives the values from PlayerMovement and use them here.

    private Animator _animator;
    private Rigidbody _rb;
    private AudioSource _footstepsSFX;
    private AudioSource _footstepsSprintFX;
    private AudioSource _heavyBreathingFX;
    private float _ogSpeed;
    private float _sprintMultiplier;
    private float _speed;
    private Transform _transform;
    private PlayerStats _playerStats;

    public bool IsMoving { get; private set; }

    public Movement(
        Animator animator,
        Rigidbody rb,
        AudioSource footstepsSFX,
        AudioSource footstepsSprintFX,
        AudioSource heavyBreathingFX,
        float ogSpeed,
        float sprintMultiplier,
        Transform transform,
        PlayerStats playerStats)
    {
        _animator = animator;
        _rb = rb;
        _footstepsSFX = footstepsSFX;
        _footstepsSprintFX = footstepsSprintFX;
        _heavyBreathingFX = heavyBreathingFX;
        _ogSpeed = ogSpeed;
        _sprintMultiplier = sprintMultiplier;
        _transform = transform;
        _playerStats = playerStats;
        _speed = ogSpeed;
    }

    public void MoveAndSprint(float x, float z)
    {
        //Basic movement
        Vector3 dir = (_transform.right * x + _transform.forward * z).normalized;

        _animator.SetFloat("xMov", x);
        _animator.SetFloat("zMov", z);
        _rb.velocity = dir * _speed;

        if (dir.magnitude == 0)
        {
            _rb.velocity = Vector3.zero;
            IsMoving = true;
        }

        IsMoving = dir.magnitude > 0;


        //Sprint
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && _playerStats.canSprint && IsMoving;

        if (isSprinting)
        {
            _animator.SetBool("isSprinting", true);
            _speed = _ogSpeed * _sprintMultiplier;
            _playerStats.UseStamina();
            _playerStats.staminaIsBeingConsumed = true;
        }
        else
        {
            _animator.SetBool("isSprinting", false);
            _speed = _ogSpeed;
            _playerStats.RecoverStaminaFromZero();
            _playerStats.RecoverIncompletedStamina();
            _playerStats.staminaIsBeingConsumed = false;
        }

        //SFX

        //Exhausted
        if (!_playerStats.canSprint && !_heavyBreathingFX.isPlaying)
        {
            _heavyBreathingFX.enabled = true;
        }
        if (_playerStats.canSprint)
        {
            _heavyBreathingFX.enabled = false;
        }


        //Walking and sprinting SFX
        if (isSprinting && IsMoving)
        {
            if (!_footstepsSprintFX.isPlaying)
                _footstepsSprintFX.Play();

            if (_footstepsSFX.isPlaying)
                _footstepsSFX.Stop();
        }
        else if (!isSprinting)
        {
            if (IsMoving)
            {
                _animator.SetBool("isMoving", true);
                if (!_footstepsSFX.isPlaying)
                    _footstepsSFX.Play();
                if (_footstepsSprintFX.isPlaying)
                    _footstepsSprintFX.Stop();
            }
            else
            {
                _animator.SetBool("isMoving", false);
                if (_footstepsSFX.isPlaying)
                    _footstepsSFX.Stop();
            }
        }
        else
        {
            if (_footstepsSprintFX.isPlaying)
                _footstepsSprintFX.Stop();
        }
    }
}
