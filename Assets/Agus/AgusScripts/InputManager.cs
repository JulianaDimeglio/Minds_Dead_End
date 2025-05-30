using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InventoryCarouselUI inventoryUI;
    [SerializeField] private FirstPersonMovement player;

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
                inventoryUI.Open(); // Esto activa el objeto y ejecuta Awake()
                UIStateManager.Instance.SetState(UIState.Inventory);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            player.originalSpeed = player.speed;
            player.originalbobFrequency = player.bobFrequency;
            player.speed *= 1.7f;
            player.bobFrequency *= 2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            player.speed = player.originalSpeed;
            player.bobFrequency = player.originalbobFrequency;
        }
    }
}
