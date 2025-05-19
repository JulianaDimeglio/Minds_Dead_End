using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoopDoorClose : MonoBehaviour
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
                    door.Close();
                    door.Lock();
                    Debug.Log($"Door '{id}' closed.");
                }
                else
                {
                    Debug.LogWarning($"Door with ID '{id}' not found.");
                }
            }
        }
    }
}
