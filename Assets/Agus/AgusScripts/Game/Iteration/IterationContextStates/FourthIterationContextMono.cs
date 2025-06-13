using System.Collections.Generic;
using UnityEngine;

public class FourthIterationContextMono : MonoBehaviour
{
    [Header("Iteration 3 references")]
    public GameObject calendar;
    public GameObject padlock;
    public GameObject tommyNote;
    public ClockAnimate clock;
    public GameObject margaret;

    [Header("IDs de puertas a cerrar, abrir, bloquear o desbloquear")]
    public List<string> doorsToCloseIDs;
    public List<string> doorsToOpenIDs;
    public List<string> doorsToLockIDs;
    public List<string> doorsToUnlockIDs;

    public FourthIterationContext ToPlain()
    {
        return new FourthIterationContext
        {
            calendar = this.calendar,
            padlock = this.padlock,
            margaret = this.margaret,
            clock = this.clock,
            tommyNote = this.tommyNote,
            doorsToCloseIDs = this.doorsToCloseIDs,
            doorsToOpenIDs = this.doorsToOpenIDs,
            doorsToLockIDs = this.doorsToLockIDs,
            doorsToUnlockIDs = this.doorsToUnlockIDs
        };
    }
}