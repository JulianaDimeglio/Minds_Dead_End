using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("Shake Settings")]
    [SerializeField] private AnimationCurve intensityCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float maxRotationAngle = 5f; // máximo ángulo de rotación en grados

    private Vector3 _originalPos;
    private Quaternion _originalRot;
    private Coroutine _shakeRoutine;

    private void Awake()
    {
        Instance = this;
        _originalPos = transform.localPosition;
        _originalRot = transform.localRotation;
    }

    public void Shake(float duration, float magnitude)
    {
        if (_shakeRoutine != null)
            StopCoroutine(_shakeRoutine);
        _shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float progress = elapsed / duration;
            float currentIntensity = intensityCurve.Evaluate(progress) * magnitude;

            // Shake de posición
            Vector3 randomOffset = Random.insideUnitSphere * currentIntensity;
            Vector3 targetPos = _originalPos + randomOffset;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * lerpSpeed);

            // Shake de rotación
            Vector3 randomEuler = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ) * (maxRotationAngle * currentIntensity);

            Quaternion targetRot = _originalRot * Quaternion.Euler(randomEuler);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * lerpSpeed);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Vuelve a posición y rotación original suavemente
        while (Vector3.Distance(transform.localPosition, _originalPos) > 0.01f ||
               Quaternion.Angle(transform.localRotation, _originalRot) > 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _originalPos, Time.deltaTime * lerpSpeed);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _originalRot, Time.deltaTime * lerpSpeed);
            yield return null;
        }

        transform.localPosition = _originalPos;
        transform.localRotation = _originalRot;
        _shakeRoutine = null;
    }
}