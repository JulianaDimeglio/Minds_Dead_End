using UnityEngine;

/// <summary>
/// Stores and resets the original transform of a GameObject.
/// Attach this to any object that should return to its original position between loops.
/// </summary>
public class TransformResetter : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        _originalScale = transform.localScale;

        LoopManager.Instance.OnLoopChanged += ResetTransform;
    }

    private void OnDestroy()
    {
        if (LoopManager.Instance != null)
            LoopManager.Instance.OnLoopChanged -= ResetTransform;
    }

    private void ResetTransform(int currentIteration)
    {
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
        transform.localScale = _originalScale;
    }
}