using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]

    [Header("Movement")]
    [SerializeField]
    private float _ogSpeed;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _sprintMultiplier = 1.5f;
    private float _xAxis, _zAxis;
    public bool isSprinting;

    private PlayerStats _playerStats;

    public AudioSource footstepsSFX;
    public AudioSource footstepsSprintFX;
    public AudioSource heavyBreathingFX;

    Animator _animator;

    public int zAxisDirection = 1;

    private bool _isMoving;

    public bool isMoving => _isMoving;

    public bool canMove;

    private Rigidbody _rb;

    private void Start()
    {
        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStats>();

        isSprinting = false;

        _animator = GetComponentInChildren<Animator>();

        heavyBreathingFX.enabled = false;

        canMove = true;

        _rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical") * zAxisDirection;

        if (canMove)
        {
            Movement(_xAxis, _zAxis);
        }
        
        Sprint();

    }

    private void Movement(float x, float z)
    {
        Vector3 dir = (transform.right * x + transform.forward * z).normalized;

        _animator.SetFloat("xMov", x);
        _animator.SetFloat("zMov", z);

        _rb.velocity = dir * _speed;

        if (dir.magnitude == 0)
        {
            _rb.velocity = Vector3.zero;
        }
            

        _isMoving = dir.magnitude > 0;

        if (!isSprinting)
        {
            if (_isMoving)
            {
                _animator.SetBool("isMoving", true);
                if (!footstepsSFX.isPlaying)
                    footstepsSFX.Play();

                // El otro sonido no esté sonando
                if (footstepsSprintFX.isPlaying)
                    footstepsSprintFX.Stop();
            }
            else
            {
                _animator.SetBool("isMoving", false);
                if (footstepsSFX.isPlaying)
                    footstepsSFX.Stop();
            }
        }
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _playerStats.canSprint)
        {
            isSprinting = true;
            _animator.SetBool("isSprinting", true);
            _speed = _ogSpeed * _sprintMultiplier;
            _playerStats.UseStamina();
            _playerStats.staminaIsBeingConsumed = true;
        }
        else
        {
            isSprinting = false;
            _animator.SetBool("isSprinting", false);
            _speed = _ogSpeed;
            _playerStats.RecoverStaminaFromZero();
            _playerStats.RecoverIncompletedStamina();
            _playerStats.staminaIsBeingConsumed = false;
        }

        //NOTA:
        //Error anterior: se empezaba a reproducir una vez por frame, dado que canSprint era falso en cada frame
        //hasta que se llenara la stamina nuevamente. Ahora solo se ejecuta mientras sea falso y a su vez
        //no se este reproduciendo ya.
        if (!_playerStats.canSprint && !heavyBreathingFX.isPlaying)
        {
            heavyBreathingFX.enabled = true;
        }

        if (_playerStats.canSprint)
        {
            heavyBreathingFX.enabled = false;
        }

        if (isSprinting && (_xAxis != 0 || _zAxis != 0))
        {
            if (!footstepsSprintFX.isPlaying)
                footstepsSprintFX.Play();

            // SFX caminar apagado
            if (footstepsSFX.isPlaying)
                footstepsSFX.Stop();
        }
        else
        {
            if (footstepsSprintFX.isPlaying)
                footstepsSprintFX.Stop();
        }
    }
}
