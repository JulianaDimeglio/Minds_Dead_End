using UnityEngine;

public interface IVisibleEntity
{
    Transform GetVisionTarget();
    void OnSeen();
    void OnVisible(); 
    void OnNotVisible();  
    float GetVisibilityRange();
}
