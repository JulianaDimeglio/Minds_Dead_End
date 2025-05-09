using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VisibilityDetector : MonoBehaviour
{
    private IPlayerVision visionSystem;
    private IVisibleEntity visibleEntity;

    void Start()
    {
        visionSystem = FindObjectOfType<PlayerVisionSystem>();
        visibleEntity = GetComponent<IVisibleEntity>();
        if (visionSystem != null && visibleEntity != null)
            visionSystem.RegisterVisibleEntity(visibleEntity);
    }

    void OnDestroy()
    {
        if (visionSystem != null && visibleEntity != null)
            visionSystem.UnregisterVisibleEntity(visibleEntity);
    }
}