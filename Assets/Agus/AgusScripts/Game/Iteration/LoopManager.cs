using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the global loop/iteration system.
/// Tracks the current iteration number, determines whether conditions are met to advance,
/// and triggers events to reconfigure the environment accordingly.
/// </summary>
public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance { get; private set; }

    /// <summary>
    /// The current loop iteration (starts at 1).
    /// </summary>
    public int CurrentIteration { get; private set; } = 1;

    /// <summary>
    /// Event fired whenever the loop restarts or advances to a new iteration.
    /// Passes the current iteration number to all listeners.
    /// </summary>
    public event Action<int> OnLoopChanged;

    private bool _conditionMet = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Sets whether the condition required to advance the loop has been fulfilled.
    /// This should be set by puzzles, triggers, or other gameplay logic.
    /// </summary>
    /// <param name="value">True if the loop condition is met, false otherwise.</param>
    public void SetConditionMet(bool value)
    {
        _conditionMet = value;
    }

    /// <summary>
    /// Attempts to advance to the next loop iteration.
    /// If the condition was met, the iteration increases.
    /// If not, the same iteration is repeated from the beginning.
    /// </summary>
    public void TryAdvanceLoop()
    {
        if (_conditionMet)
        {
            CurrentIteration++;
            _conditionMet = false;
            Debug.Log($"[LoopManager] Advancing to iteration {CurrentIteration}");
        }
        else
        {
            Debug.Log($"[LoopManager] Repeating iteration {CurrentIteration}");
        }

        _conditionMet = false;

        // Notify all systems that the loop has (re)started
        OnLoopChanged?.Invoke(CurrentIteration);

    }
}