using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inspectionUI;

    [Header("Post Processing")]
    [SerializeField] private PostProcessVolume postVolume;

    private DepthOfField depthOfField;

    [Header("Inventory Blur Settings")]
    [SerializeField] private float inventoryFocusDistance = 0.15f;
    [SerializeField] private float inventoryAperture = 2f;
    [SerializeField] private float inventoryFocalLength = 15f;

    [Header("Inspection Blur Settings")]
    [SerializeField] private float inspectionFocusDistance = 0.8f;
    [SerializeField] private float inspectionAperture = 1.5f;
    [SerializeField] private float inspectionFocalLength = 25f;

    [Header("Default Settings (No Blur)")]
    [SerializeField] private float defaultFocusDistance = 6.38f;
    [SerializeField] private float defaultAperture = 3f;
    [SerializeField] private float defaultFocalLength = 15f;

    private void Start()
    {
        postVolume.profile.TryGetSettings(out depthOfField);
        RestoreDefaultBlur();
    }

    public void ShowInventory()
    {
        inventoryUI?.SetActive(true);
        ApplyBlur(inventoryFocusDistance, inventoryAperture, inventoryFocalLength);
    }

    public void HideInventory()
    {
        inventoryUI?.SetActive(false);
        if (inspectionUI != null && !inspectionUI.activeSelf)
            RestoreDefaultBlur();
    }

    public void ShowInspection()
    {
        inspectionUI?.SetActive(true);
        ApplyBlur(inspectionFocusDistance, inspectionAperture, inspectionFocalLength);
    }

    public void HideInspection()
    {
        inspectionUI?.SetActive(false);
        if (inventoryUI != null && !inventoryUI.activeSelf)
            RestoreDefaultBlur();
    }

    private void ApplyBlur(float focusDistance, float aperture, float focalLength)
    {
        if (depthOfField == null) return;

        depthOfField.focusDistance.value = focusDistance;
        depthOfField.aperture.value = aperture;
        depthOfField.focalLength.value = focalLength;
    }

    private void RestoreDefaultBlur()
    {
        ApplyBlur(defaultFocusDistance, defaultAperture, defaultFocalLength);
    }
}