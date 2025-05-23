using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemID;
    public string displayName;
    [TextArea] public string description;

    [Header("3D Representation")]
    public GameObject modelPrefab;
    [SerializeField] private Vector3 initialRotationEuler;
    [SerializeField] private Vector3 initialScaleEuler = new Vector3(3,3,3);
    public Quaternion InitialRotation => Quaternion.Euler(initialRotationEuler);
    public Vector3 InitialScale => new Vector3(initialScaleEuler.x, initialScaleEuler.y, initialScaleEuler.z);

    // (Opcional) ícono para UI
    public Sprite icon;

    // Si en el futuro algunos ítems son consumibles
    public bool isUsable = false;
}
