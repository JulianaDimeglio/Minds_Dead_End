using UnityEngine;

public interface IInspectable
{
    void OnInspect();
    string GetDescription();
    Vector3 GetOriginalWorldPosition();
    Quaternion GetOriginalWorldRotation();
    Transform GetOriginalParent();
    int GetOriginalLayer();

    bool CanBeCollected { get; }
}