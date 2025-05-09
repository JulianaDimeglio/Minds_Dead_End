using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    // This class controls how the crosshair works and informs the player what objects are interactable

    public Image crosshairImage;
    public float checkDistance = 3f;
    public LayerMask interactLayer;

    [Header("Sizes:")]
    public Vector3 normalScale = Vector3.one;
    [SerializeField] private Vector3 hoverScale;


    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
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
        crosshairImage.transform.localScale = normalScale;

    }
}