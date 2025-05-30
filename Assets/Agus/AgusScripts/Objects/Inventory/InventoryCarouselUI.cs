using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryCarouselUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform inspectionContainer;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private Image useImage;

    private GameObject currentModel;
    private bool isOpen = false;
    public static InventoryCarouselUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!gameObject.activeSelf || !isOpen) return;

        if (Input.GetKeyDown(KeyCode.A))
            InventoryManager.Instance.PreviousItem();

        if (Input.GetKeyDown(KeyCode.D))
            InventoryManager.Instance.NextItem();

        if (Input.GetKeyDown(KeyCode.Space))
            InventoryManager.Instance.UseCurrentItem();

        if (Input.GetKeyDown(KeyCode.Tab))
            Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        isOpen = true;

        // Activar desenfoque
        BlurManager blur = FindObjectOfType<BlurManager>();
        if (blur != null)
            blur.ShowInventory();

        InventoryManager.Instance.ItemChanged += UpdateUI;
        UpdateUI(InventoryManager.Instance.CurrentItem);
    }

    public void Close()
    {
        InventoryManager.Instance.ItemChanged -= UpdateUI;
        ClearModel();
        gameObject.SetActive(false);
        isOpen = false;

        // Desactivar desenfoque
        BlurManager blur = FindObjectOfType<BlurManager>();
        if (blur != null)
            blur.HideInventory();

        if (UIStateManager.Instance.CurrentState == UIState.Inventory)
            UIStateManager.Instance.SetState(UIState.None);
    }

    private void UpdateUI(InventoryItemData data)
    {
        ClearModel();

        if (data == null || data.modelPrefab == null)
        {
            itemNameText.text = "No Item";
            itemDescriptionText.text = "";
            return;
        }

        currentModel = Instantiate(data.modelPrefab, inspectionContainer);
        SetLayerRecursively(currentModel.transform, LayerMask.NameToLayer("UI"));

        Vector3 visualCenter = GetRendererCenter(currentModel);
        Vector3 pivot = currentModel.transform.position;
        Vector3 offset = visualCenter - pivot;

        currentModel.transform.localPosition = -inspectionContainer.InverseTransformVector(offset);
        currentModel.transform.localRotation = data.InitialRotation;
        currentModel.transform.localScale = data.InitialScale;

        itemNameText.text = data.displayName;
        itemDescriptionText.text = data.description;

        if (data.isUsable)
        {
            useText.text = "Usar";
            useImage.enabled = true;
        }
        else
        {
            useText.text = "";
            useImage.enabled = false;
        }
    }

    private void ClearModel()
    {
        if (currentModel != null)
            Destroy(currentModel);
    }

    private void SetLayerRecursively(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayerRecursively(child, layer);
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
}