using UnityEngine;

public class ProximityDetectionIcons : MonoBehaviour
{
    public float maxDistance = 3f;
    public LayerMask interactableLayer;
    private InteractableIcon currentTarget;
    public Transform player;

    void Update()
    {
        InteractableIcon[] allInteractables = FindObjectsOfType<InteractableIcon>();
        InteractableIcon closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (InteractableIcon i in allInteractables)
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