using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a door that can be opened and closed via code or triggers.
/// </summary>
public class Door : MonoBehaviour, IInteraction
{
    [Tooltip("Unique identifier for this door.")]
    public string Id;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] bool _isLocked = false;
    private DoorSoundManager _doorSoundManager;

    private bool _isOpen = false;
    private bool _isAnimating = false;

    private void Awake()
    {
        _doorSoundManager = DoorSoundManager.Instance;

        DoorManager.Instance?.RegisterDoor(this);
        animator = gameObject.GetComponent<Animator>();

    }

    public void Open()
    {
        if (_isOpen) return;

        _isOpen = true;
        _isAnimating = true;
        animator?.SetTrigger("Open");
        Debug.Log($"Door '{Id}' opened.");
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        _isAnimating = true;
        animator?.SetTrigger("Close");
        Debug.Log($"Door '{Id}' closed.");
    }

    public void Toggle()
    {
        Debug.Log($"Door '{Id}' toggled. IsOpen: {_isOpen}, IsLocked: {_isLocked}, IsAnimating: {_isAnimating}");
        if (_isLocked)
        {
            Debug.Log($"Door '{Id}' is locked.");
            return;
        }
        if ( !_isAnimating)
        {            
            if (_isOpen) Close();
            else Open();
        }
    }

    public void Lock()
    {

        _isLocked = true;
        Debug.Log($"Door '{Id}' locked.");
    }

    public void Unlock()
    {
        _isLocked = false;
        Debug.Log($"Door '{Id}' unlocked.");
    }

    public void TriggerInteraction()
    {
        Toggle();
    }

    public void OnAnimationComplete()
    {
        _isAnimating = false;
        Debug.Log($"Door '{Id}' finished animation");
    }

    public void PlayOpenSoundFromEvent()
    {
        DoorSoundManager.Instance?.PlayOpen(transform.position);
    }

    public void PlayCloseSoundFromEvent()
    {
        DoorSoundManager.Instance?.PlayClose(transform.position);
    }
}