using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

/// <summary>
/// Manages interactive doors in the environment that can be opened, closed, or toggled by ID.
/// </summary>
public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance { get; private set; }
    private Dictionary<string, Door> _doors = new Dictionary<string, Door>();
    [Tooltip("All doors that can be controlled programmatically.")]

    private Dictionary<string, Door> _doorMap = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void RegisterDoor(Door door)
    {
        if (door == null || string.IsNullOrEmpty(door.Id)) return;

        if (!_doors.ContainsKey(door.Id))
        {
            Debug.Log($"REGISTERED DOOR {door.Id}");
            _doors.Add(door.Id, door);
        }
        else
        {
            Debug.LogWarning($"[DoorManager] Door with ID '{door.Id}' is already registered.");
        }
    }

    public Door GetDoorById(string id)
    {
        _doors.TryGetValue(id, out var door);
        return door;
    }
}