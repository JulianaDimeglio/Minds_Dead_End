using UnityEngine;
using UnityEngine.Video;
using System;

[RequireComponent(typeof(VideoPlayer))]
public class TVVideoController : MonoBehaviour
{
    [Header("Video Clips")]
    [SerializeField] private VideoClip mainClip;
    [SerializeField] private VideoClip staticNoiseClip;

    private VideoPlayer videoPlayer;
    private bool isMainVideoPlaying = false;

    public bool IsMainVideoPlaying => isMainVideoPlaying;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        videoPlayer.loopPointReached += OnMainVideoFinished;
    }

    /// <summary>
    /// Reproduce el clip principal una sola vez y luego ejecuta una acción opcional.
    /// </summary>
    public void PlayMainVideo(Action onFinished = null)
    {
        if (mainClip == null)
        {
            Debug.LogWarning("No main video clip assigned.");
            return;
        }

        videoPlayer.clip = mainClip;
        videoPlayer.isLooping = false;
        videoPlayer.Play();
        isMainVideoPlaying = true;

        // Acción que se ejecuta cuando termina
        videoPlayer.loopPointReached += (_) =>
        {
            isMainVideoPlaying = false;
            onFinished?.Invoke();
        };
    }

    /// <summary>
    /// Reproduce el video de ruido en loop.
    /// </summary>
    public void PlayStaticVideo()
    {
        if (staticNoiseClip == null)
        {
            Debug.LogWarning("No static noise video clip assigned.");
            return;
        }

        videoPlayer.clip = staticNoiseClip;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }

    /// <summary>
    /// Detiene la TV (como si se apagara).
    /// </summary>
    public void StopVideoPlayback()
    {
        videoPlayer.Stop();
        isMainVideoPlaying = false;
    }

    /// <summary>
    /// Reproduce cualquier clip manualmente.
    /// </summary>
    public void SwitchToClip(VideoClip newClip, bool loop = false)
    {
        if (newClip == null)
        {
            Debug.LogWarning("Trying to switch to null VideoClip.");
            return;
        }

        videoPlayer.clip = newClip;
        videoPlayer.isLooping = loop;
        videoPlayer.Play();
        isMainVideoPlaying = !loop;
    }

    private void OnMainVideoFinished(VideoPlayer vp)
    {
        isMainVideoPlaying = false;
    }
}