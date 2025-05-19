using UnityEngine;

public class AudioBackground : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;

    private AudioSource audioSource;

    private void Awake()
    {
        // Evita duplicados
        if (FindObjectsOfType<AudioBackground>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D (global)
        audioSource.volume = 1f;
        audioSource.Play();
    }
}