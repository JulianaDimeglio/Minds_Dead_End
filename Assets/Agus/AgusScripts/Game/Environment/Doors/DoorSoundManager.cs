using UnityEngine;

public class DoorSoundManager : MonoBehaviour
{
    public static DoorSoundManager Instance;

    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private AudioClip lockedClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayOpen(Vector3 position)
    {
        if (openClip != null)
            AudioSource.PlayClipAtPoint(openClip, position);
    }

    public void PlayClose(Vector3 position)
    {
        if (closeClip != null)
            AudioSource.PlayClipAtPoint(closeClip, position);
    }

    public void PlayLocked(Vector3 position)
    {
        if (lockedClip != null)
            AudioSource.PlayClipAtPoint(lockedClip, position);
    }

    public void PlayDoorLocked(Vector3 position)
    {
        if (doorLocked != null)
            AudioSource.PlayClipAtPoint(doorLocked, position);
    }
}