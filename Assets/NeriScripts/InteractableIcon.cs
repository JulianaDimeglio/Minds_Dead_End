using UnityEngine;
using System.Collections;

public class InteractableIcon : MonoBehaviour
{
    [SerializeField] GameObject interanctableIcon;

    private bool isDisabled = false; 

    void Update()
    {
        if (isDisabled) return;

        FaceCameraIfActive(interanctableIcon);
    }

    private void FaceCameraIfActive(GameObject icon)
    {
        if (icon != null && icon.activeSelf && Camera.main != null)
        {
            gameObject.transform.LookAt(Camera.main.transform);
        }
    }

    public void ShowIcons()
    {
        if (isDisabled) return;
        if (interanctableIcon != null)
            StartCoroutine(FadeIcon(interanctableIcon, true));
    }

    public void HideIcons()
    {
        if (interanctableIcon != null)
            StartCoroutine(FadeIcon(interanctableIcon, false));
    }

    private IEnumerator FadeIcon(GameObject icon, bool fadeIn)
    {
        icon.SetActive(true);
        CanvasGroup cg = icon.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = icon.AddComponent<CanvasGroup>();

        float duration = 0.3f;
        float startAlpha = cg.alpha;
        float endAlpha = fadeIn ? 1f : 0f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, t / duration);
            yield return null;
        }

        cg.alpha = endAlpha;

        if (!fadeIn)
            icon.SetActive(false);
    }

    
    public void DisableInteraction()
    {
        isDisabled = true;
        HideIcons();
        if (interanctableIcon != null) interanctableIcon.SetActive(false);
        this.enabled = false; // Opcional: para desactivar completamente el script
    }
}