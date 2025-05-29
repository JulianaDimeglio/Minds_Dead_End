using Game.Puzzles;
using System.Collections.Generic;
using UnityEngine;

public class SecondIterationState : LoopStateBase
{
    private readonly SecondIterationContext context;

    public SecondIterationState(SecondIterationContext context)
    {
        this.context = context;
    }

    public override void Configure()
    {
        context.paintingPrefab.SetActive(true);
        context.photoPieces.SetActive(true);
        // Configurar puertas según IDs
        DoorManager.Instance.UnlockDoors(context.doorsToUnlockIDs);
        DoorManager.Instance.LockDoors(context.doorsToLockIDs);
        DoorManager.Instance.CloseDoors(context.doorsToCloseIDs);
        DoorManager.Instance.OpenDoors(context.doorsToOpenIDs);

        Debug.Log("[SecondIterationState] Iteration configured.");
    }

    public override void CleanIteration()
    {
        context.paintingPrefab.SetActive(false);
        context.photoPieces.SetActive(false);
        Debug.Log("[SecondIterationState] Cleaning iteration...");
    }
}
