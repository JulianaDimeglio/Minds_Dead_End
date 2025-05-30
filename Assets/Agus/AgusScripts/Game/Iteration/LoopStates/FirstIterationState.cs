using Game.Puzzles;
using System.Collections.Generic;
using UnityEngine;

public class FirstIterationState : LoopStateBase
{
    private readonly FirstIterationContext context;

    public FirstIterationState(FirstIterationContext context)
    {
        this.context = context;
    }

    public override void Configure()
    {
        context.flashlight.SetActive(true);
        context.childJumpscareTrigger.SetActive(true);
        context.triggerDoorCloseChild.SetActive(true);
        // Configurar puertas según IDs
        DoorManager.Instance.UnlockDoors(context.doorsToUnlockIDs);
        DoorManager.Instance.LockDoors(context.doorsToLockIDs);
        DoorManager.Instance.CloseDoors(context.doorsToCloseIDs);
        DoorManager.Instance.OpenDoors(context.doorsToOpenIDs);

        Debug.Log("[FirstIterationState] Iteration configured.");
    }

    public override void CleanIteration()
    {
        context.childJumpscareTrigger.SetActive(false);
        Debug.Log("[FirstIterationState] Cleaning iteration...");
    }
}
