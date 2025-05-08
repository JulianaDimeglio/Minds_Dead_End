using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all breakable glass objects in the environment, such as mirrors or windows.
/// </summary>
public class GlassManager : MonoBehaviour
{
    [Tooltip("All breakable glass objects in the scene, indexed by unique ID.")]
    [SerializeField] private List<BreakableGlass> glassObjects = new();

    private Dictionary<string, BreakableGlass> _glassMap = new();

    private void Awake()
    {
        foreach (var glass in glassObjects)
        {
            if (glass != null && !_glassMap.ContainsKey(glass.Id))
                _glassMap[glass.Id] = glass;
        }
    }

    /// <summary>
    /// Shatters a glass object by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the glass object to shatter.</param>
    public void Shatter(string id)
    {
        if (_glassMap.TryGetValue(id, out var glass))
        {
            glass.Shatter();
        }
        else
        {
            Debug.LogWarning($"Glass object with ID '{id}' not found.");
        }
    }
}