using UnityEngine;

public interface IInspectable
{
    void OnInspect();
    string Description { get; }

    string Id { get; }
    string Name { get; }
    Vector3 OriginalWorldPosition { get; }
    Quaternion OriginalWorldRotation { get; }
    Transform OriginalParent { get; }
    int OriginalLayer { get; }

    bool CanBeCollected { get; }
}