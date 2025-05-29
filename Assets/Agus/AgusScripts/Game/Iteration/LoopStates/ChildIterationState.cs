using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildIterationState : LoopStateBase
{

    private readonly FirstIterationContext context;

    public ChildIterationState(FirstIterationContext context)
    {
        this.context = context;
    }
    public override void Configure()
    {
        // here you can configure the first iteration
    }
    public override void CleanIteration()
    {
        // here you can clean the first iteration
        // this method is called when the iteration ends
        // for example, you can disable the game objects that were enabled in the Configure method
        
    }
}
