using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform modelContainer;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private GameObject currentModel;
    private bool isOpen = false;

    private void Start()
    {
        //InventoryManager.Instance.ItemChanged += UpdateUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        //if (InventoryManager.Instance != null)
            //InventoryManager.Instance.ItemChanged -= UpdateUI;
    }

    private void Update()
    {
        if (!isOpen) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            InventoryManager.Instance.PreviousItem();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            InventoryManager.Instance.NextItem();

        if (Input.GetKeyDown(KeyCode.E))
            InventoryManager.Instance.UseCurrentItem();

        if (Input.GetMouseButtonDown(1)) // Right click
            Close();

        if (Input.GetKeyDown(KeyCode.Tab))
            Close();
    }

    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        //UpdateUI(InventoryManager.Instance.CurrentItem);
    }

    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
        ClearCurrentModel();
    }

    private void UpdateUI(InspectableItem item)
    {
        if (item == null)
        {
            descriptionText.text = "Empty";
            ClearCurrentModel();
            return;
        }

        descriptionText.text = item.GetDescription();
        ClearCurrentModel();

        GameObject model = Instantiate(((MonoBehaviour)item).gameObject, modelContainer);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        model.layer = LayerMask.NameToLayer("UI");
        SetLayerRecursively(model.transform, model.layer);

        currentModel = model;
    }

    private void ClearCurrentModel()
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
}