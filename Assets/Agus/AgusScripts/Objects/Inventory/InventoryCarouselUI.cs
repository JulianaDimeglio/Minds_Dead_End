using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class InventoryCarouselUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform inspectionContainer; // Reutilizado del sistema de inspección
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI useText;

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
        if (!gameObject.activeSelf) return;

        if (!isOpen) return;

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
        // Ya no reposicionamos el container
        InventoryManager.Instance.ItemChanged += UpdateUI;
        UpdateUI(InventoryManager.Instance.CurrentItem);
    }

    public void Close()
    {
        InventoryManager.Instance.ItemChanged -= UpdateUI;
        ClearModel();
        gameObject.SetActive(false);
        isOpen = false;
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

        // Calcular el centro visual del modelo
        Vector3 visualCenter = GetRendererCenter(currentModel);
        Vector3 pivot = currentModel.transform.position;
        Vector3 offset = visualCenter - pivot;

        // Aplicar posición para que el centro visual quede en el origen del contenedor
        currentModel.transform.localPosition = -inspectionContainer.InverseTransformVector(offset);

        // Aplicar rotación desde el ScriptableObject o una por defecto
        currentModel.transform.localRotation = data.InitialRotation;


        // Escalar de forma uniforme
        currentModel.transform.localScale = data.InitialScale;

        // Mostrar textos
        itemNameText.text = data.displayName;
        itemDescriptionText.text = data.description;
        if (data.isUsable)
        {
            useText.text = "(Space) Use";
        }
        else
        {
            useText.text = "";
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