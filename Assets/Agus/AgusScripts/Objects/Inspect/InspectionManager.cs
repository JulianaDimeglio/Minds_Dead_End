using UnityEngine;
using System.Collections;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Transform inspectionContainer;
    [SerializeField] private InspectionUI inspectionUI;

    [Header("Settings")]
    [SerializeField] private float moveDuration = 5f;

    private InspectableItem currentItem;
    private bool isInspecting = false;
    private Coroutine currentMoveCoroutine;

    public bool IsInspecting => isInspecting;
    private bool canRotate = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (!isInspecting || currentItem == null) return;

        if (Input.GetKeyDown(KeyCode.Mouse1) && canRotate)
        {
            StopInspect();
            return;
        }

        if (isInspecting && Input.GetKeyDown(KeyCode.Space))
        {
            TryStoreItem();
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rotationSpeed = 2.5f;
        if (!canRotate) return;

        inspectionContainer.Rotate(Vector3.up, -mouseX * rotationSpeed, Space.World);
        inspectionContainer.Rotate(Vector3.right, mouseY * rotationSpeed, Space.World);
    }

    public void StartInspect(InspectableItem item)
    {
        UIStateManager.Instance.SetState(UIState.Inspecting);
        isInspecting = true;
        currentItem = item;

        inspectionContainer.localRotation = Quaternion.identity;
        currentItem.OnInspect();

        Transform itemTransform = ((MonoBehaviour)item).transform;

        Vector3 visualCenter = GetRendererCenter(itemTransform.gameObject);
        Vector3 pivot = itemTransform.position;
        Vector3 offset = visualCenter - pivot;
        Vector3 targetLocalPos = -inspectionContainer.InverseTransformVector(offset);

        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveToInspection(itemTransform, targetLocalPos));

        if (inspectionUI != null)
            inspectionUI.Show(item.Description, item.Name, item.CanBeCollected);
    }

    public void StopInspect()
    {
        if (currentItem == null) return;

        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        inspectionUI?.Hide();

        Transform itemTransform = ((MonoBehaviour)currentItem).transform;

        Vector3 targetPos = currentItem.OriginalWorldPosition;
        Quaternion targetRot = currentItem.OriginalWorldRotation;
        Transform originalParent = currentItem.OriginalParent;
        int originalLayer = currentItem.OriginalLayer;

        currentMoveCoroutine = StartCoroutine(MoveBackToWorld(itemTransform, targetPos, targetRot, originalParent, originalLayer));

        if (UIStateManager.Instance.CurrentState == UIState.Inspecting)
            UIStateManager.Instance.SetState(UIState.None);
    }

    private IEnumerator MoveToInspection(Transform item, Vector3 targetLocalPos)
    {
        Vector3 startPos = item.position;
        Quaternion startRot = item.rotation;

        Vector3 targetWorldPos = inspectionContainer.TransformPoint(targetLocalPos);
        Quaternion targetRot = inspectionContainer.rotation;

        item.SetParent(null);
        yield return StartCoroutine(MoveTransform(item, startPos, startRot, targetWorldPos, targetRot, moveDuration));

        canRotate = true;

        item.gameObject.layer = LayerMask.NameToLayer("InspectedObject");

        item.SetParent(inspectionContainer);
        item.localPosition = targetLocalPos;
        item.localRotation = Quaternion.identity;
    }

    private IEnumerator MoveBackToWorld(Transform item, Vector3 targetPosition, Quaternion targetRotation, Transform originalParent, int originalLayer)
    {
        canRotate = false;
        item.SetParent(null);

        Vector3 startPos = item.position;
        Quaternion startRot = item.rotation;

        item.gameObject.layer = originalLayer;
        yield return StartCoroutine(MoveTransform(item, startPos, startRot, targetPosition, targetRotation, moveDuration));

        item.SetParent(originalParent);
        currentItem = null;
        isInspecting = false;
    }

    private Vector3 GetRendererCenter(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return obj.transform.position;

        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        return bounds.center;
    }

    public void CollectCurrentItem()
    {
        if (currentItem == null || !currentItem.CanBeCollected) return;

        Debug.Log("Item collected. (Inventory system pending)");
        StopInspect();
    }

    private IEnumerator MoveTransform(Transform target, Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, float duration)
    {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
                target.position = Vector3.Lerp(startPos, endPos, t);
                target.rotation = Quaternion.Slerp(startRot, endRot, t);
                yield return null;
            }

            target.position = endPos;
            target.rotation = endRot;
    }

    private void TryStoreItem()
    {
        if (currentItem == null || !currentItem.CanBeCollected)
            return;


        // 👉 MOSTRAR MENSAJE SOLO PARA LA LINTERNA
        if (itemData.itemID == "flashlight_item")
        {
            if (SubtitleManager.Instance != null)
                SubtitleManager.Instance.ShowSubtitle("Presiona F para encender la linterna");
        }

        if (itemData.itemID == "edithPhoto")
        {
            var jumpScare = FindObjectOfType<JumpscareOldLady>();
            if (jumpScare != null)
                jumpScare.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
            // Guardar ID en el inventario
            InventoryManager.Instance.AddItem(currentItem.Id);

        InventoryManager.Instance.AddItem(itemData.itemID);

        isInspecting = false;
        canRotate = false;

        inspectionUI?.Hide();

        var action = currentItem.GetComponent<ICollectable>();
        action?.OnCollect();
        // Remover objeto del mundo
        Destroy(((MonoBehaviour)currentItem).gameObject);
        currentItem = null;

        if (UIStateManager.Instance.CurrentState == UIState.Inspecting)
            UIStateManager.Instance.SetState(UIState.None);
    }
}