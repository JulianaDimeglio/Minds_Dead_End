using System.Collections.Generic;
using UnityEngine;

public class FirstIterationContextMono : MonoBehaviour
{
    [Header("Referencias del puzzle")]
    public GameObject flashlight;
    public GameObject childJumpscareTrigger;
    public GameObject triggerDoorCloseChild;

    [Header("IDs de puertas a cerrar, abrir, bloquear o desbloquear")]
    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;

    public FirstIterationContext ToPlain()
    {
        return new FirstIterationContext
        {
            flashlight = this.flashlight,
            childJumpscareTrigger = this.childJumpscareTrigger,
            triggerDoorCloseChild = this.triggerDoorCloseChild,
            doorsToCloseIDs = this.doorsToCloseIDs,
            doorsToOpenIDs = this.doorsToOpenIDs,
            doorsToLockIDs = this.doorsToLockIDs,
            doorsToUnlockIDs = this.doorsToUnlockIDs
        };
    }
}