using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage;
    public float checkDistance = 3f;
    public LayerMask interactLayer;

    [Header("Colors:")]
    public Color defaultColor = Color.white;
    public Color interactColor = Color.green;

    [Header("Sizes:")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1f);


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
                crosshairImage.transform.localScale = hoverScale;
                return;
            }
        }
        crosshairImage.color = defaultColor;
        crosshairImage.transform.localScale = normalScale;

    }
}