using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableCollectable: MonoBehaviour, ICollectable
{
    public bool IsCollected { get; private set; } = false;
    public void OnCollect()
    {
        Destroy(gameObject.GetComponent<InspectableItem>()?.interactableIcon);
    }
}
