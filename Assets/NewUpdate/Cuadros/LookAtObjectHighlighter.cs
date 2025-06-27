using UnityEngine;

public class LookAtTextureChanger : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;
    public LayerMask interactableLayer;
    public Texture newTexture;
    public AudioClip lookSound;
    public float delay = 1f; 

    private bool hasActivated = false;
    private AudioSource audioSource;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (hasActivated) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                StartCoroutine(DelayedChange(renderer));
                hasActivated = true;
            }
        }
    }

    System.Collections.IEnumerator DelayedChange(Renderer renderer)
    {
        yield return new WaitForSeconds(delay);

        renderer.material.mainTexture = newTexture;

        if (lookSound != null)
            audioSource.PlayOneShot(lookSound);
    }
}
