using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class FPCameraEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform cameraWrapper;

    [Header("Headbob Settings")]
    [SerializeField] private float bobFrequency = 1.8f;
    [SerializeField] private float bobHorizontalAmplitude = 0.05f;
    [SerializeField] private float bobVerticalAmplitude = 0.025f;
    [SerializeField] private float bobTiltAmplitude = 1.0f;
    [SerializeField] private float bobTransitionSpeed = 6f;

    [Header("Breathing Settings")]
    [SerializeField] private float breathFrequency = 0.8f;
    [SerializeField] private float breathVerticalAmplitude = 0.01f;
    [SerializeField] private float breathTiltAmplitude = 0.3f;

    [Header("Tilt Settings")]
    [SerializeField] private float cameraTiltAmplitude = 1f;
    [SerializeField] private float cameraTiltSpeed = 6f;

    private PlayerMovement playerMovement;
    private Vector3 cameraInitialPos;
    private float breathTimer = 0f;
    private float bobTimer = 0f;
    private float currentTiltZ = 0f;
    private float originalBobFrequency;


    public float BobFrequency
    {
        get => bobFrequency;
        set => bobFrequency = value;
    }

    public float OriginalBobFrequency
    {
        get => originalBobFrequency;
        set => originalBobFrequency = value;
    }

    private void Start()
    {
        originalBobFrequency = bobFrequency;
        playerMovement = GetComponent<PlayerMovement>();
        cameraInitialPos = playerCamera.localPosition;
    }

    private void Update()
    {
        bool isMoving = playerMovement.MoveDirection.magnitude > 0.1f;


        Quaternion baseRotation = playerCamera.localRotation;
        Vector3 baseEuler = baseRotation.eulerAngles;
        if (baseEuler.x > 180f) baseEuler.x -= 360f; // normalizar pitch

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            float horizontalBob = Mathf.Cos(bobTimer) * bobHorizontalAmplitude;

            float sinValue = Mathf.Sin(bobTimer * 2f);
            float verticalBob = sinValue < 0
                ? -Mathf.Pow(-sinValue, 2f)
                : Mathf.Pow(sinValue, 0.5f);

            verticalBob *= bobVerticalAmplitude;

            Vector3 bobOffset = new Vector3(horizontalBob, verticalBob, 0f);
            Vector3 targetPosition = cameraInitialPos + bobOffset;

            float targetTilt = Mathf.Sin(bobTimer) * cameraTiltAmplitude;
            currentTiltZ = Mathf.Lerp(currentTiltZ, targetTilt, Time.deltaTime * cameraTiltSpeed);

            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetPosition, Time.deltaTime * bobTransitionSpeed);
            cameraWrapper.localRotation = Quaternion.Euler(0f, 0f, currentTiltZ);
        }
        else
        {
            currentTiltZ = Mathf.Lerp(currentTiltZ, 0f, Time.deltaTime * cameraTiltSpeed);
            cameraWrapper.localRotation = Quaternion.Euler(0f, 0f, currentTiltZ);

            breathTimer += Time.deltaTime * breathFrequency;

            float breathY = Mathf.Sin(breathTimer) * breathVerticalAmplitude;
            float breathTiltZ = Mathf.Sin(breathTimer * 0.8f) * breathTiltAmplitude;

            Vector3 breathOffset = new Vector3(0f, breathY, 0f);
            Vector3 targetPosition = cameraInitialPos + breathOffset;
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetPosition, Time.deltaTime * bobTransitionSpeed);

            Quaternion originalRot = playerCamera.localRotation;
            Quaternion tiltOnly = Quaternion.Euler(0f, 0f, breathTiltZ);
            Quaternion finalRot = originalRot * tiltOnly;

            playerCamera.localRotation = Quaternion.Slerp(originalRot, finalRot, Time.deltaTime * bobTransitionSpeed);
        }
    }

    public float GetCurrentTiltZ() => currentTiltZ;
}
