using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    private void Start()
    {
        _speed = _ogSpeed;

        _playerStats = GetComponent<PlayerStats>();

        isSprinting = false;

    }

    void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        Movement(_xAxis, _zAxis);

        Sprint();

    }

    private void Movement(float x, float z)
    {
        Vector3 dir = (transform.right * x + transform.forward * z).normalized;

        transform.position += dir * _speed * Time.deltaTime;

        bool isMoving = dir.magnitude > 0;

        if (!isSprinting)
        {
            if (isMoving)
            {
                if (!footstepsSFX.isPlaying)
                    footstepsSFX.Play();

                // Asegurarse de que el otro sonido no esté sonando
                if (footstepsSprintFX.isPlaying)
                    footstepsSprintFX.Stop();
            }
            else
            {
                if (footstepsSFX.isPlaying)
                    footstepsSFX.Stop();
            }
        }
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _playerStats.CanSprint())
        {
            isSprinting = true;
            _speed = _ogSpeed * _sprintMultiplier;
            _playerStats.UseStamina();
        }
        else
        {
            isSprinting = false;
            _speed = _ogSpeed;
            _playerStats.RecoverStamina();
        }

        if (isSprinting && (_xAxis != 0 || _zAxis != 0))
        {
            if (!footstepsSprintFX.isPlaying)
                footstepsSprintFX.Play();

            // Asegurarse de que el sonido de caminar esté apagado
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
