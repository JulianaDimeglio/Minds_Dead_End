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
        context.triggerTvOn.SetActive(true);
        context.triggerTvLook.SetActive(true);
        context.margaret.SetActive(false);
        DoorManager.Instance.UnlockDoors(context.doorsToUnlockIDs);
        DoorManager.Instance.LockDoors(context.doorsToLockIDs);
        DoorManager.Instance.CloseDoors(context.doorsToCloseIDs);
        DoorManager.Instance.OpenDoors(context.doorsToOpenIDs);

        Debug.Log("[ThirdIterationState] Iteration configured.");
    }

    public override void CleanIteration()
    {
        context.margaret.SetActive(false);
        context.triggerTvOn.SetActive(false);
        context.triggerTvLook.SetActive(false);
        Debug.Log("[ThirdIterationState] Cleaning iteration...");
    }
}
