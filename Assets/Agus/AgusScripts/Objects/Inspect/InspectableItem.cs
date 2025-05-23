using UnityEngine;

public class InspectableItem : MonoBehaviour, IInspectable
{
    [SerializeField] private bool canBeCollected = true;
    [SerializeField] private InventoryItemData itemData;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private int originalLayer;
    public Vector3 GetOriginalWorldPosition() => originalPosition;
    public Quaternion GetOriginalWorldRotation() => originalRotation;
    public Transform GetOriginalParent() => originalParent;
    public int GetOriginalLayer() => originalLayer;

    public InventoryItemData GetItemData() => itemData;
    public string GetDescription() => itemData != null ? itemData.description : "";
    public string GetDisplayName() => itemData != null ? itemData.displayName : "Unnamed Item";
    public bool CanBeCollected => canBeCollected;
    public void OnInspect()
    {
        // Guardamos estado global (posición y rotación)
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalLayer = gameObject.layer;
    }



}