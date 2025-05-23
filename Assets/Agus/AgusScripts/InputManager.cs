using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InventoryCarouselUI inventoryUI;

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
    }
}
