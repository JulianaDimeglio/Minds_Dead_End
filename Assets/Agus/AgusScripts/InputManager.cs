using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InventoryCarouselUI inventoryUI;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private FPCameraEffects cameraEffects;
    [SerializeField] private RaycastInteractionManager raycastManager;
    [SerializeField] private FlashlightToggle flashlight;
    [SerializeField] private KeyCode flashlightKey = KeyCode.F;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIStateManager.Instance.CurrentState == UIState.Inventory)
            {
                inventoryUI.Close();
                UIStateManager.Instance.SetState(UIState.None);
            }
            else
            {
                inventoryUI.Open();
                UIStateManager.Instance.SetState(UIState.Inventory);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            string message = $"Next Iteration will be Iteration 0";
            LoopManager.Instance?.SetCurrentIterationDebug(0);
            SubtitleManager.Instance.ShowSubtitle(message);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string message = $"Next Iteration will be Iteration 1";
            LoopManager.Instance?.SetCurrentIterationDebug(1);
            SubtitleManager.Instance.ShowSubtitle(message);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string message = $"Next Iteration will be Iteration 2";
            LoopManager.Instance?.SetCurrentIterationDebug(2);
            SubtitleManager.Instance.ShowSubtitle(message);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            string message = $"Next Iteration will be Iteration 3";
            LoopManager.Instance?.SetCurrentIterationDebug(3);
            SubtitleManager.Instance.ShowSubtitle(message);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            string message = $"Next Iteration will be Iteration 4";
            LoopManager.Instance?.SetCurrentIterationDebug(4);
            SubtitleManager.Instance.ShowSubtitle(message);
        }
        if (Input.GetKeyDown(interactKey))
        {
            raycastManager.TryInteract();
        }

        if (Input.GetKeyDown(flashlightKey))
        {
            flashlight.ToggleFlashLight();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            movement.OriginalSpeed = movement.Speed;
            cameraEffects.OriginalBobFrequency = cameraEffects.BobFrequency;

            movement.Speed *= 1.3f;
            cameraEffects.BobFrequency *= 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            movement.Speed = movement.OriginalSpeed;
            cameraEffects.BobFrequency = cameraEffects.OriginalBobFrequency;
        }
    }
}
