using UnityEngine;

public class IntroSubtitleTrigger : MonoBehaviour
{
    private bool shown = false;

    private void Start()
    {
        if (!shown && SubtitleManager.Instance != null)
        {
            SubtitleManager.Instance.ShowSubtitle("Presiona TAB para abrir el inventario");
            shown = true;
        }
    }
}