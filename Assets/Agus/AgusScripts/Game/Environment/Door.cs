using UnityEngine;

/// <summary>
/// Represents a door that can be opened and closed via code or triggers.
/// </summary>
public class Door : MonoBehaviour
{
    [Tooltip("Unique identifier for this door.")]
    public string Id;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private bool _isOpen = false;

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;
        animator?.SetTrigger("Open");
        Debug.Log($"Door '{Id}' opened.");
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        animator?.SetTrigger("Close");
        Debug.Log($"Door '{Id}' closed.");
    }

    public void Toggle()
    {
        if (_isOpen) Close();
        else Open();
    }
}