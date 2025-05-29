using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIterationState : LoopStateBase
{
    private readonly FirstIterationContext context;

    public ShadowIterationState(FirstIterationContext context)
    {
        this.context = context;
    }
    public override void Configure()
    {
        Enable("ScalePuzzle");
        Disable("Shadow");
        Enable("Child");
        Enable("Mother");
    }

    public override void CleanIteration()
    {
        throw new System.NotImplementedException();
    }
}
