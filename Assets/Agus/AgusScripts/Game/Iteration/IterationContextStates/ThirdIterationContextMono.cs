using System.Collections.Generic;
using UnityEngine;

public class ThirdIterationContextMono : MonoBehaviour
{
    [Header("Iteration 3 references")]
    public GameObject triggerTvOn;
    public GameObject triggerTvLook;
    public GameObject margaret;

    [Header("IDs de puertas a cerrar, abrir, bloquear o desbloquear")]
    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;

    public ThirdIterationContext ToPlain()
    {
        return new ThirdIterationContext
        {
            triggerTvOn = this.triggerTvOn,
            triggerTvLook = this.triggerTvLook,
            margaret = this.margaret,
            doorsToCloseIDs = this.doorsToCloseIDs,
            doorsToOpenIDs = this.doorsToOpenIDs,
            doorsToLockIDs = this.doorsToLockIDs,
            doorsToUnlockIDs = this.doorsToUnlockIDs
        };
    }
}