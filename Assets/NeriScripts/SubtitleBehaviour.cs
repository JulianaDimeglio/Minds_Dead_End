using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitleText;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        SubtitleUI.Instance.ShowSubtitle(subtitleText);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        SubtitleUI.Instance.HideSubtitle();
    }
}

