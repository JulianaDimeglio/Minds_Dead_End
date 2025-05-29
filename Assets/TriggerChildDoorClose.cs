using UnityEngine;

public class TriggerChildDoorClose : MonoBehaviour
{
    [SerializeField] private Door targetDoor; // Asign� la puerta en el inspector

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (targetDoor != null)
        {
            targetDoor.Close();
            targetDoor.Lock();
            Debug.Log($"[TriggerChildDoorClose] Puerta '{targetDoor.name}' cerrada.");
        }

        // Desactivar el trigger
        gameObject.SetActive(false);
    }
}