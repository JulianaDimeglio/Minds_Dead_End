using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform hand;
    [SerializeField] float handRotationSpeed = 8f;
    [SerializeField][Range(0.0f, 5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    [Header("Headbob Settings")]
    [SerializeField] float bobFrequency = 1.8f;
    [SerializeField] float bobHorizontalAmplitude = 0.05f;
    [SerializeField] float bobVerticalAmplitude = 0.025f;
    [SerializeField] float bobTiltAmplitude = 1.0f;
    [SerializeField] float bobTransitionSpeed = 6f;


    [Header("Whip Effect")]
    [SerializeField] float whipStrength = 8f;
    [SerializeField] float whipDampSpeed = 6f;
    [SerializeField] float footTiltAmplitude = 1.2f;


    [SerializeField] float stepRotStrength = 4f;
    [SerializeField] float stepRotDamp = 6f;

    [Header("Breathing Settings")]
    [SerializeField] float breathFrequency = 0.8f;
    [SerializeField] float breathVerticalAmplitude = 0.01f;
    [SerializeField] float breathTiltAmplitude = 0.3f;


    float breathTimer = 0f;
    float stepRotAngle = 0f;
    float stepRotVelocity = 0f;
    Vector3 cameraInitialPos;
    float bobTimer;
    float lastBobSin;
    float whipTilt;
    float whipVelocity;

    float rotation_x_axis;
    float rotation_y_axis;

    private bool _mouseInitialized = false;
    [SerializeField] private Transform cameraWrapper;
    [SerializeField] private float cameraTiltAmplitude = 1f;
    [SerializeField] private float cameraTiltSpeed = 6f;
    private float currentTiltZ = 0f;

    [Header("Footstep Audio")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private List<AudioClip> footstepClips;
    [SerializeField] private float tiltThreshold = 0.1f; // Valor cercano a 0 para detectar el paso
    private bool hasStepped = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraInitialPos = playerCamera.localPosition;

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        UpdateMouse();
        UpdateMove();
        HandleHeadBob();
    }

    private void ResetMouseDelta()
    {
        Input.GetAxis("Mouse X");
        Input.GetAxis("Mouse Y");

        currentMouseDelta = Vector2.zero;
        currentMouseDeltaVelocity = Vector2.zero;
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);

        Quaternion cameraTargetRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        playerCamera.localRotation = cameraTargetRotation;
        Quaternion handTargetRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        hand.localRotation = Quaternion.Lerp(hand.localRotation, handTargetRotation, handRotationSpeed);

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMouse2()
    {
        rotation_x_axis += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotation_y_axis -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotation_x_axis = Mathf.Clamp(rotation_x_axis, -90f, 90f);

        hand.localRotation = Quaternion.Euler(-rotation_x_axis, rotation_y_axis, 0f);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, rotation_y_axis, 0), mouseSensitivity * Time.deltaTime);

        playerCamera.localRotation = Quaternion.Lerp(playerCamera.localRotation, Quaternion.Euler(-rotation_x_axis, 0, 0), mouseSensitivity * Time.deltaTime);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        velocityY += gravity * Time.deltaTime;
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;
        if (targetDir.magnitude > 0.01f)
        {
            // Suaviza solo al comenzar a moverse
            currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * (1f / moveSmoothTime));
        }
        else
        {
            // Se detiene en sec
            currentDir = Vector2.zero;
        }
        controller.Move(velocity * Time.deltaTime);
        if (!isGrounded && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    void HandleHeadBob()
    {
        bool isMoving = currentDir.magnitude > 0.1f;

        if (isMoving)
        {
            float targetTilt = Mathf.Sin(bobTimer) * cameraTiltAmplitude;
            currentTiltZ = Mathf.Lerp(currentTiltZ, targetTilt, Time.deltaTime * cameraTiltSpeed);
            Debug.Log(currentTiltZ);
            bobTimer += Time.deltaTime * bobFrequency;

            float horizontalBob = Mathf.Cos(bobTimer) * bobHorizontalAmplitude;

            float sinValue = Mathf.Sin(bobTimer * 2f);
            float verticalBob;
            if (sinValue < 0)
            {
                verticalBob = sinValue * sinValue * -1f;
            }
            else
            {
                verticalBob = Mathf.Pow(sinValue, 0.5f);
            }

            verticalBob *= bobVerticalAmplitude;

            // Aplicar posición
            Vector3 bobOffset = new Vector3(horizontalBob, verticalBob, 0f);
            Vector3 targetPosition = cameraInitialPos + bobOffset;
            // Detectar si estamos cerca de 0 y aún no pisamos
            if (Mathf.Abs(currentTiltZ) <= tiltThreshold && !hasStepped)
            {
                PlayFootstepSound();
                hasStepped = true;
            }
            else if (Mathf.Abs(currentTiltZ) > tiltThreshold)
            {
                hasStepped = false;
            }
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetPosition, Time.deltaTime * bobTransitionSpeed);
            cameraWrapper.localRotation = Quaternion.Euler(0f, 0f, currentTiltZ);
        }
        else
        {
            currentTiltZ = Mathf.Lerp(currentTiltZ, 0f, Time.deltaTime * cameraTiltSpeed);
            cameraWrapper.localRotation = Quaternion.Euler(0f, 0f, currentTiltZ);

            // Respira suavemente
            breathTimer += Time.deltaTime * breathFrequency;

            float breathY = Mathf.Sin(breathTimer) * breathVerticalAmplitude;
            float breathTiltZ = Mathf.Sin(breathTimer * 0.8f) * breathTiltAmplitude;

            Vector3 breathOffset = new Vector3(0f, breathY, 0f);
            Vector3 targetPosition = cameraInitialPos + breathOffset;
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetPosition, Time.deltaTime * bobTransitionSpeed);

            Quaternion targetRotation = Quaternion.Euler(cameraCap + stepRotAngle, 0f, breathTiltZ);
            playerCamera.localRotation = Quaternion.Slerp(playerCamera.localRotation, targetRotation, Time.deltaTime * bobTransitionSpeed);
        }
    }

    void PlayFootstepSound()
    {
        if (footstepClips.Count == 0 || footstepSource == null)
            return;

        int index = Random.Range(0, footstepClips.Count);
        footstepSource.PlayOneShot(footstepClips[index]);
    }
}
