using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Movement _movementHandler;

    [Header("Movement")]
    [SerializeField]
    private float _ogSpeed;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _sprintMultiplier = 1.5f;
    private float _xAxis, _zAxis;
    private Rigidbody _rb;
    private bool _isMoving;
    private PlayerStats _playerStats;
    private Animator _animator;

    public AudioSource footstepsSFX;
    public AudioSource footstepsSprintFX;
    public AudioSource heavyBreathingFX;
    public int zAxisDirection = 1;
    public bool isMoving => _isMoving;
    public bool canMove;
    public bool isSprinting;


    private void Start()
    {
        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStats>();

        isSprinting = false;

        _animator = GetComponentInChildren<Animator>();

        heavyBreathingFX.enabled = false;

        canMove = true;

        _rb = GetComponent<Rigidbody>();


        //Se le pasan los valores de las variables al constrcutor.
        _movementHandler = new Movement(
            _animator,
            _rb,
            footstepsSFX,
            footstepsSprintFX,
            heavyBreathingFX,
            _ogSpeed,
            _sprintMultiplier,
            transform,
            _playerStats
        );
    }

    void Update()
    {
        //Se toman los inputs.
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical") * zAxisDirection; //Se multiplica al eje vertical por la direccion del zAxis para tener control sobre ese eje de manera independiente.

        if (canMove)
        {
            _movementHandler.MoveAndSprint(_xAxis, _zAxis);

        }
    }

    //Metodo que altera el zAxis en el momento que el jugador interactua con el objeto revertidor.
    public void InvertZAxis(bool state)
    {
        if (state)
        {
            zAxisDirection *= -1;
        }
        else
        {
            zAxisDirection *= -1;
        }
    }


    //private void Movement(float x, float z)
    //{
    //    Vector3 dir = (transform.right * x + transform.forward * z).normalized;

    //    _animator.SetFloat("xMov", x);
    //    _animator.SetFloat("zMov", z);

    //    _rb.velocity = dir * _speed;

    //    if (dir.magnitude == 0)
    //    {
    //        _rb.velocity = Vector3.zero;
    //    }


    //    _isMoving = dir.magnitude > 0;

    //    if (!isSprinting)
    //    {
    //        if (_isMoving)
    //        {
    //            _animator.SetBool("isMoving", true);
    //            if (!footstepsSFX.isPlaying)
    //                footstepsSFX.Play();

    //            // El otro sonido no esté sonando
    //            if (footstepsSprintFX.isPlaying)
    //                footstepsSprintFX.Stop();
    //        }
    //        else
    //        {
    //            _animator.SetBool("isMoving", false);
    //            if (footstepsSFX.isPlaying)
    //                footstepsSFX.Stop();
    //        }
    //    }
    //}

    //void Sprint()
    //{
    //    if (Input.GetKey(KeyCode.LeftShift) && _playerStats.canSprint)
    //    {
    //        isSprinting = true;
    //        _animator.SetBool("isSprinting", true);
    //        _speed = _ogSpeed * _sprintMultiplier;
    //        _playerStats.UseStamina();
    //        _playerStats.staminaIsBeingConsumed = true;
    //    }
    //    else
    //    {
    //        isSprinting = false;
    //        _animator.SetBool("isSprinting", false);
    //        _speed = _ogSpeed;
    //        _playerStats.RecoverStaminaFromZero();
    //        _playerStats.RecoverIncompletedStamina();
    //        _playerStats.staminaIsBeingConsumed = false;
    //    }

    //    //NOTA:
    //    //Error anterior: se empezaba a reproducir una vez por frame, dado que canSprint era falso en cada frame
    //    //hasta que se llenara la stamina nuevamente. Ahora solo se ejecuta mientras sea falso y a su vez
    //    //no se este reproduciendo ya.
    //    if (!_playerStats.canSprint && !heavyBreathingFX.isPlaying)
    //    {
    //        heavyBreathingFX.enabled = true;
    //    }

    //    if (_playerStats.canSprint)
    //    {
    //        heavyBreathingFX.enabled = false;
    //    }

    //    if (isSprinting && (_xAxis != 0 || _zAxis != 0))
    //    {
    //        if (!footstepsSprintFX.isPlaying)
    //            footstepsSprintFX.Play();

    //        // SFX caminar apagado
    //        if (footstepsSFX.isPlaying)
    //            footstepsSFX.Stop();
    //    }
    //    else
    //    {
    //        if (footstepsSprintFX.isPlaying)
    //            footstepsSprintFX.Stop();
    //    }
    //}
}
