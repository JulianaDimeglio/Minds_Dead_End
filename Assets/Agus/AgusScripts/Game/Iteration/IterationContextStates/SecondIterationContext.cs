using System.Collections.Generic;
using UnityEngine;

public class SecondIterationContext : IterationContextBase
{
    public GameObject paintingPrefab;
    public GameObject photoPieces;

    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;
}