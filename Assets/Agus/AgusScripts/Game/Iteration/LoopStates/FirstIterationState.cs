using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstIterationState : LoopStateBase
{
    public override void Configure() {
        Debug.Log("AAAASDDDDDDDDDD MOTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        Disable("ButtonsManager");
        Disable("ButtonsPuzzle");
        Disable("ScalePuzzle");
        Disable("Child");
        Disable("Mother");
    }

}
