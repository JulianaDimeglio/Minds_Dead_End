using UnityEngine;

public class InspectableItem : MonoBehaviour, IInspectable
{
    [SerializeField] private InventoryItemData itemData;
    public InteractableIcon interactableIcon;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private int originalLayer;
    public Vector3 OriginalWorldPosition => originalPosition;
    public Quaternion OriginalWorldRotation => originalRotation;
    public Transform OriginalParent => originalParent;
    public int OriginalLayer => originalLayer;

    public InventoryItemData ItemData => itemData;
    public string Description => itemData != null ? itemData.description : "";
    public string Name => itemData != null ? itemData.displayName : "Unnamed Item";
    public bool CanBeCollected => itemData.isCollectable;

    public string Id => itemData.itemID;

    public bool CanBeUsed => itemData.isUsable;
    public void OnInspect()
    {
        interactableIcon?.HideIcons();
        // Guardamos estado global (posición y rotación)
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalLayer = gameObject.layer;
    }

    public virtual void OnGrab()
    {
        return;
    }

}