using UnityEngine;
using Game.Puzzles;
using System.Collections;

public class ReticleChanger : MonoBehaviour
{
    public Camera mainCamera;
    public float rayDistance = 2.3f;
    public LayerMask raycastMask;

    public ReticleManager reticleManager;

    private bool showingTemporaryReticle = false;

    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, raycastMask))
        {
            if (hit.collider.GetComponent<PhotoFramePuzzle>())
            {
                if (!showingTemporaryReticle)
                    reticleManager.SetReticle(reticleManager.interactSprite);
            }
            else if (hit.collider.GetComponent<InspectableItem>())
            {
                if (!showingTemporaryReticle)
                    reticleManager.SetReticle(reticleManager.interactSprite);
            }
            else if (hit.collider.GetComponent<LightSwitchMarker>())
            {
                if (!showingTemporaryReticle)
                    reticleManager.SetReticle(reticleManager.lightSwitchSprite);
            }
            else if (hit.collider.GetComponent<Door>())
            {
                Door door = hit.collider.GetComponent<Door>();

                if (door.IsLocked && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartCoroutine(ShowReticleForSeconds(reticleManager.lockedDoorSprite, 1f));
                }
                else if (!door.IsLocked)
                {
                    if (!showingTemporaryReticle)
                        reticleManager.SetReticle(reticleManager.interactSprite);
                }
                else
                {
                    if (!showingTemporaryReticle)
                        reticleManager.SetReticle(reticleManager.defaultSprite);
                }
            }
            else if (hit.collider.GetComponent<SimpleOpenClose>())
            {
                if (!showingTemporaryReticle)
                    reticleManager.SetReticle(reticleManager.interactSprite);
            }
            else
            {
                if (!showingTemporaryReticle)
                    reticleManager.SetReticle(reticleManager.defaultSprite);
            }
        }
        else
        {
            if (!showingTemporaryReticle)
                reticleManager.SetReticle(reticleManager.defaultSprite);
        }
    }

    private IEnumerator ShowReticleForSeconds(Sprite sprite, float duration)
    {
        showingTemporaryReticle = true;
        reticleManager.SetReticle(sprite);
        yield return new WaitForSeconds(duration);
        reticleManager.SetReticle(reticleManager.defaultSprite);
        showingTemporaryReticle = false;
    }
}