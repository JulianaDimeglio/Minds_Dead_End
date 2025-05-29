using System.Collections.Generic;
using UnityEngine;

public class ThirdIterationContextMono : MonoBehaviour
{
    [Header("IDs de puertas a cerrar, abrir, bloquear o desbloquear")]
    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;

    public ThirdIterationContext ToPlain()
    {
        return new ThirdIterationContext
        {
            doorsToCloseIDs = this.doorsToCloseIDs,
            doorsToOpenIDs = this.doorsToOpenIDs,
            doorsToLockIDs = this.doorsToLockIDs,
            doorsToUnlockIDs = this.doorsToUnlockIDs
        };
    }
}