using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage;
    public Color defaultColor = Color.white;
    public Color interactColor = Color.green;
    public float checkDistance = 3f;
    public LayerMask interactLayer;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        crosshairImage.color = defaultColor;
    }

    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, checkDistance, interactLayer))
        {
            if (hit.collider.GetComponent<IInteraction>() != null)
            {
                crosshairImage.color = interactColor;
                return;
            }
        }
        crosshairImage.color = defaultColor;
    }
}
