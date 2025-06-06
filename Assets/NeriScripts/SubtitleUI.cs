using UnityEngine;
using TMPro;

public class SubtitleUI : MonoBehaviour
{
    public static SubtitleUI Instance;

    public TextMeshProUGUI subtitleText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSubtitle(string text)
    {
        subtitleText.text = text;
    }

    public void HideSubtitle()
    {
        subtitleText.text = "";
    }
}