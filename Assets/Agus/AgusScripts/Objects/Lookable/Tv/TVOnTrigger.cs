using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVOnTrigger : MonoBehaviour
{
    [SerializeField] private TVController tvController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tvController.TurnOnDelayed(3.2f);
            Destroy(this); // solo se activa una vez
        }
    }
}
