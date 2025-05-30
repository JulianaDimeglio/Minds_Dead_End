using UnityEngine;

public class ProximityDetectionIcons : MonoBehaviour
{
    public float maxDistance = 3f;
    public LayerMask interactableLayer;
    private Interactable currentTarget;
    public Transform player; // Tu cámara o cabeza del jugador

    void Update()
    {
        Interactable[] allInteractables = FindObjectsOfType<Interactable>();
        Interactable closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Interactable i in allInteractables)
        {
            float distance = Vector3.Distance(player.position, i.transform.position);
            if (distance < maxDistance && distance < closestDistance)
            {
                closest = i;
                closestDistance = distance;
            }
        }

        if (closest != null)
        {
            if (currentTarget != closest)
            {
                if (currentTarget != null) currentTarget.HideIcons();
                currentTarget = closest;
                currentTarget.ShowIcons();
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.HideIcons();
                currentTarget = null;
            }
        }
    }
}