using UnityEngine;

/// <summary>
/// Abstract base class for loop iteration states with shared helpers.
/// </summary>
public abstract class LoopStateBase : ILoopState
{
    public abstract void Configure();
    public abstract void CleanIteration();

    protected void Disable(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            obj.SetActive(false);
            Debug.Log($"[LoopStateBase] Disabled '{objectName}'");
        }
        else
        {
            Debug.LogWarning($"[LoopStateBase] Could not find '{objectName}' to disable.");
        }
    }

    protected void Enable(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            obj.SetActive(true);
            Debug.Log($"[LoopStateBase] Enabled '{objectName}'");
        }
        else
        {
            Debug.LogWarning($"[LoopStateBase] Could not find '{objectName}' to enable.");
        }
    }
}