using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages activation of grouped GameObjects per loop iteration.
/// Only one iteration group is active at a time.
/// Ideal for structuring scenes with persistent iteration-specific content.
/// </summary>
public class IterationGroupActivator : MonoBehaviour
{
    [Tooltip("One root GameObject per iteration. Index 0 = iteration 1, Index 1 = iteration 2, etc.")]
    [SerializeField] private List<GameObject> iterationRoots;

    private int _currentActiveIndex = -1;

    /// <summary>
    /// Activates only the group corresponding to the given iteration number.
    /// Deactivates the previously active group, if any.
    /// </summary>
    /// <param name="iteration">The iteration number (1-based index).</param>
    public void ActivateOnly(int iteration)
    {
        int targetIndex = iteration - 1;

        // Deactivate previously active group
        if (_currentActiveIndex >= 0 && _currentActiveIndex < iterationRoots.Count)
        {
            iterationRoots[_currentActiveIndex].SetActive(false);
        }

        // Activate the new group
        if (targetIndex >= 0 && targetIndex < iterationRoots.Count)
        {
            iterationRoots[targetIndex].SetActive(true);
            _currentActiveIndex = targetIndex;
        }
        else
        {
            Debug.LogWarning($"[IterationGroupActivator] No group assigned for iteration {iteration}.");
        }
    }
}