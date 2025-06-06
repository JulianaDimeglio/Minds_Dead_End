using UnityEngine;

public enum PlayerState
{
    Normal,
    Focus,
    Cutscene,
    GhostChase,
    Paralyzed
}

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance { get; private set; }

    public PlayerState CurrentState { get; private set; } = PlayerState.Normal;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        ExitState(CurrentState);
        EnterState(newState);

        CurrentState = newState;
    }

    private void EnterState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Focus:
                PlayerInputBlocker.Instance?.BlockAll();
                break;

            case PlayerState.Cutscene:
                PlayerInputBlocker.Instance?.BlockAll();
                break;

            case PlayerState.Paralyzed:
                PlayerInputBlocker.Instance?.BlockAll();
                break;
            case PlayerState.Normal:
                PlayerInputBlocker.Instance?.UnblockAll();
                break;
        }
    }

    private void ExitState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Focus:
            case PlayerState.Cutscene:
            case PlayerState.Paralyzed:
                PlayerInputBlocker.Instance?.UnblockAll();
                break;
        }
    }

    public bool IsBusy => CurrentState != PlayerState.Normal;
}