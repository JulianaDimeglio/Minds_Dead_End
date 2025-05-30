using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public GameObject eyeIcon;
    public GameObject earIcon;
    public bool canObserve = false;
    public bool canListen = false;

    private bool isDisabled = false; 

    void Update()
    {
        if (isDisabled) return;

        FaceCameraIfActive(eyeIcon);
        FaceCameraIfActive(earIcon);
    }

    private void FaceCameraIfActive(GameObject icon)
    {
        if (icon != null && icon.activeSelf && Camera.main != null)
        {
            Vector3 direction = Camera.main.transform.position - icon.transform.position;
            direction.y = 0;
            icon.transform.rotation = Quaternion.LookRotation(-direction);
        }
    }

    public void ShowIcons()
    {
        if (isDisabled) return;

        if (canObserve && eyeIcon != null)
            StartCoroutine(FadeIcon(eyeIcon, true));

        if (canListen && earIcon != null)
            StartCoroutine(FadeIcon(earIcon, true));
    }

    public void HideIcons()
    {
        if (eyeIcon != null)
            StartCoroutine(FadeIcon(eyeIcon, false));

        if (earIcon != null)
            StartCoroutine(FadeIcon(earIcon, false));
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
        if (eyeIcon != null) eyeIcon.SetActive(false);
        if (earIcon != null) earIcon.SetActive(false);
        this.enabled = false; // Opcional: para desactivar completamente el script
    }
}