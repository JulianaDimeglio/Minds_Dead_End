using System.Collections.Generic;
using UnityEngine;

public class FirstIterationContext : IterationContextBase
{
    public GameObject flashlight;
    public GameObject childJumpscareTrigger;
    public GameObject triggerDoorCloseChild;

    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;
}