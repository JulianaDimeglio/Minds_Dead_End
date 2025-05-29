using System.Collections.Generic;
using UnityEngine;

public class SecondIterationContextMono : MonoBehaviour
{
    [Header("Referencias del puzzle")]
    public GameObject paintingPrefab;
    public GameObject photoPieces;

    [Header("IDs de puertas a cerrar, abrir, bloquear o desbloquear")]
    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;

    public SecondIterationContext ToPlain()
    {
        return new SecondIterationContext
        {
            paintingPrefab = this.paintingPrefab,
            photoPieces = this.photoPieces,
            doorsToCloseIDs = this.doorsToCloseIDs,
            doorsToOpenIDs = this.doorsToOpenIDs,
            doorsToLockIDs = this.doorsToLockIDs,
            doorsToUnlockIDs = this.doorsToUnlockIDs
        };
    }
}