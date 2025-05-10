using System.Collections.Generic;
using UnityEngine;

public class PlayerVisionSystem : MonoBehaviour, IPlayerVision
{
    [SerializeField] private float visionAngle = 60f;
    [SerializeField] private float maxVisionDistance = 10f;
    [SerializeField] private LayerMask obstructionMask;

    private Camera _camera;
    private List<IVisibleEntity> visibleEntities = new();

    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        foreach (var entity in visibleEntities)
        {
            if (IsVisible(entity))
                entity.OnSeen();
        }
    }

    public void RegisterVisibleEntity(IVisibleEntity entity)
    {
        if (!visibleEntities.Contains(entity))
            visibleEntities.Add(entity);
    }

    public void UnregisterVisibleEntity(IVisibleEntity entity)
    {
        if (visibleEntities.Contains(entity))
            visibleEntities.Remove(entity);
    }

    private bool IsVisible(IVisibleEntity entity)
    {
        Transform target = entity.GetVisionTarget();
        Vector3 toTarget = target.position - _camera.transform.position;

        // Proximity
        if (toTarget.magnitude > maxVisionDistance)
            return false;

        // Vision angle
        float angle = Vector3.Angle(_camera.transform.forward, toTarget.normalized);
        if (angle > visionAngle * 0.5f)
            return false;

        // Is obstructed
        if (Physics.Raycast(_camera.transform.position, toTarget.normalized, out RaycastHit hit, maxVisionDistance, obstructionMask))
        {
            if (hit.transform != target)
                return false;
        }

        return true;
    }
}