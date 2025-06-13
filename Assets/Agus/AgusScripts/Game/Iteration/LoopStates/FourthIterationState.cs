using Game.Puzzles;
using System.Collections.Generic;
using UnityEngine;

public class FourthIterationState : LoopStateBase
{
    private readonly FourthIterationContext context;

    public FourthIterationState(FourthIterationContext context)
    {
        this.context = context;
    }

    public override void Configure()
    {
        context.calendar.SetActive(true);
        context.padlock.SetActive(true);
        context.margaret.SetActive(false);
        context.clock.setClockBroken(true);
        DoorManager.Instance.UnlockDoors(context.doorsToUnlockIDs);
        DoorManager.Instance.LockDoors(context.doorsToLockIDs);
        DoorManager.Instance.CloseDoors(context.doorsToCloseIDs);
        DoorManager.Instance.OpenDoors(context.doorsToOpenIDs);

        Debug.Log("[ThirdIterationState] Iteration configured.");
    }

    public override void CleanIteration()
    {
        context.margaret.SetActive(false);
        context.clock.setClockBroken(false);
        context.padlock.SetActive(false);
        context.calendar.SetActive(false);
        Debug.Log("[ThirdIterationState] Cleaning iteration...");
    }
}
