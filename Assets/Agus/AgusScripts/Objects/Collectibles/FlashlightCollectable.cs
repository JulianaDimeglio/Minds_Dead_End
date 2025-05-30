using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightCollectable : MonoBehaviour, ICollectable
{
    public bool IsCollected { get; private set; } = false;

    public void OnCollect()
    {
        IsCollected = true;
        Destroy(gameObject);
    }
}
