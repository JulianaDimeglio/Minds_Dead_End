using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage;
    public float checkDistance = 3f;
    public LayerMask interactLayer;

    [Header("Sizes:")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1f);


    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        //casts a ray from the camera to check the distance 
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, checkDistance, interactLayer))
        {
            //if the object is interactable then it wil
            if (hit.collider.GetComponent<IInteraction>() != null)
            {
                crosshairImage.transform.localScale = hoverScale;
                return;
            }
        }
        //if the object is not interactable reset the size 
        crosshairImage.transform.localScale = normalScale; 

    }
}
