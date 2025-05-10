using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // This class hanldes how the player moves in the world.
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
    public int zAxisDirection = 1;
    public bool isMoving => _isMoving;
    public bool canMove;
    public bool isSprinting;

    [Header("SFX")]
    public AudioSource footstepsSFX;
    public AudioSource footstepsSprintFX;
    public AudioSource heavyBreathingFX;



    private void Start()
    {
        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStats>();

        isSprinting = false;

        _animator = GetComponentInChildren<Animator>();

        heavyBreathingFX.enabled = false;

        canMove = true;

        _rb = GetComponent<Rigidbody>();


        //Values sent to constructor.
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
        // Inputs.
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical") * zAxisDirection; //Se multiplica al eje vertical por la direccion del zAxis para tener control sobre ese eje de manera independiente.

        if (canMove)
        {
            _movementHandler.MoveAndSprint(_xAxis, _zAxis);

        }
    }

    // This method inverts the zAxis at the moment player interacts with the InverterObject.
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
}
