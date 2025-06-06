using UnityEngine;

public enum UIState
{
    None,
    Inspecting,
    Inventory
}

public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance { get; private set; }

    public UIState CurrentState { get; private set; } = UIState.None;

    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetState(UIState newState)
    {
        if (newState != UIState.None && !PlayerInputBlocker.Instance.canOpenUI)
        {
            return;
        }
        if (newState != UIState.None)
        {
            PlayerInputBlocker.Instance?.BlockAll();
        }
        if (isTransitioning || CurrentState == newState)
            return;


        isTransitioning = true;
        // Cerrar el estado anterior

        switch (CurrentState)
        {
            case UIState.Inspecting:
                if (InspectionManager.Instance != null)
                    InspectionManager.Instance.StopInspect();
                break;

            case UIState.Inventory:
                if (InventoryCarouselUI.Instance != null)
                    InventoryCarouselUI.Instance.Close();
                break;
        }

        CurrentState = newState;

        isTransitioning = false;
        if (newState == UIState.None)
        {
            PlayerInputBlocker.Instance?.UnblockAll();
        }
    }
    public bool IsAnyUIOpen => CurrentState != UIState.None;
}