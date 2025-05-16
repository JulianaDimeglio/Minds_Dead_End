using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIterationState : LoopStateBase
{
    public override void Configure()
    {
        Enable("ScalePuzzle");
        Disable("Shadow");
        Enable("Child");
        Enable("Mother");
    }
}
