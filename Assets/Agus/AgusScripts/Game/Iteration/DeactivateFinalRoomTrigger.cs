using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFinalRoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject finalRoomTrigger;

    private void OnTriggerEnter(Collider other)
    {
        //check if player tag 
        if (other.CompareTag("Player"))
        {
            finalRoomTrigger.SetActive(false);
        }

    }
}
