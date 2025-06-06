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
