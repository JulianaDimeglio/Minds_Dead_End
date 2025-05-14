using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoopChange : MonoBehaviour
{
    [SerializeField] List<string> doorIds;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoopManager.Instance.TryAdvanceLoop();
        }
    }
}