using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoopDoorUnlock : MonoBehaviour
{
    [SerializeField] List<string> doorIds;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var id in doorIds)
            {
                Door door = DoorManager.Instance.GetDoorById(id);
                if (door != null)
                {
                    door.Unlock();
                    Debug.Log($"Door '{id}' unlocked.");
                }
                else
                {
                    Debug.LogWarning($"Door with ID '{id}' not found.");
                }
            }
        }
    }
}
