using UnityEngine;

public class PlayerInputBlocker : MonoBehaviour
{
    public static PlayerInputBlocker Instance { get; private set; }

    public bool BlockMovement { get; private set; } = false;
    public bool BlockLook { get; private set; } = false;

    public bool canOpenUI => !BlockMovement && !BlockLook;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void BlockAll()
    {
        BlockMovement = true;
        BlockLook = true;
    }

    public void UnblockAll()
    {
        BlockMovement = false;
        BlockLook = false;
    }

    public void BlockMovementOnly(bool value) => BlockMovement = value;
    public void BlockLookOnly(bool value) => BlockLook = value;
}