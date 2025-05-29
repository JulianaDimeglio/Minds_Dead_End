using Game.Puzzles;
using System.Collections.Generic;
using UnityEngine;

public class ThirdIterationState : LoopStateBase
{
    private readonly ThirdIterationContext context;

    public ThirdIterationState(ThirdIterationContext context)
    {
        this.context = context;
    }

    public override void Configure()
    {
        DoorManager.Instance.UnlockDoors(context.doorsToUnlockIDs);
        DoorManager.Instance.LockDoors(context.doorsToLockIDs);
        DoorManager.Instance.CloseDoors(context.doorsToCloseIDs);
        DoorManager.Instance.OpenDoors(context.doorsToOpenIDs);

        Debug.Log("[SecondIterationState] Iteration configured.");
    }

    public override void CleanIteration()
    {
        Debug.Log("[SecondIterationState] Cleaning iteration...");
    }
}
