using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

/// <summary>
/// Manages interactive doors in the environment that can be opened, closed, or toggled by ID.
/// </summary>
public class DoorManager : MonoBehaviour
{
    [Tooltip("All doors that can be controlled programmatically.")]
    [SerializeField] private List<Door> doors = new();

    private Dictionary<string, Door> _doorMap = new();

    private void Awake()
    {
        foreach (var door in doors)
        {
            if (door != null && !_doorMap.ContainsKey(door.Id))
                _doorMap[door.Id] = door;
        }
    }

    public void Open(string doorId)
    {
        if (_doorMap.TryGetValue(doorId, out var door))
            door.Open();
        else
            Debug.LogWarning($"[DoorManager] Door with ID '{doorId}' not found.");
    }

    public void Close(string doorId)
    {
        if (_doorMap.TryGetValue(doorId, out var door))
            door.Close();
        else
            Debug.LogWarning($"[DoorManager] Door with ID '{doorId}' not found.");
    }

    public void Toggle(string doorId)
    {
        if (_doorMap.TryGetValue(doorId, out var door))
            door.Toggle();
        else
            Debug.LogWarning($"[DoorManager] Door with ID '{doorId}' not found.");
    }
}