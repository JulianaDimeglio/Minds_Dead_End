using System.Collections;
using UnityEngine;

public class IterationRoomManager : MonoBehaviour
{
    [SerializeField] private GameObject entryDoor;
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private PhoneInteraction phone;
    [SerializeField] private float waitBeforePhoneRings = 5f;
    [SerializeField] private GameObject triggerZone;

    public bool sequenceStarted = false;

    
    public bool playerIsInRoom { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IterationRoomEvents.PlayerEntered();
            PlayerEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IterationRoomEvents.PlayerExited();
        }
    }

    public void PlayerEntered()
    {
        if (sequenceStarted) return;
        sequenceStarted = true;
        playerIsInRoom = true;
        entryDoor.GetComponent<Door>().Close();
        entryDoor.GetComponent<Door>().Lock();
        StartCoroutine(PhoneSequence());
    }

    private IEnumerator PhoneSequence()
    {
        yield return new WaitForSeconds(waitBeforePhoneRings);

        bool shouldAdvance = LoopManager.Instance.ConditionMet;
        phone.StartRinging(shouldAdvance, OnPhoneInteractionFinished);
    }

    private void OnPhoneInteractionFinished()
    {
        exitDoor.GetComponent<Door>().Open();
        exitDoor.GetComponent<Door>().Unlock();
        triggerZone.SetActive(true);
        LoopManager.Instance.TryAdvanceLoop();
    }
}