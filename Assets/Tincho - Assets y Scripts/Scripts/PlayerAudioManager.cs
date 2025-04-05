using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    public AudioSource audioSource;
    public AudioClip footstepClip;
    //public AudioClip hurtClip;
    // Agrega más clips si necesitas

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (audioSource == null) // ← Si no está asignado en el Inspector, lo busca en el mismo GameObject
                audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFootstep()
    {
        if (footstepClip == null || audioSource == null)
        {
            Debug.LogError("¡Falta asignar `footstepClip` o `audioSource` en PlayerAudioManager!");
            return;
        }

        audioSource.PlayOneShot(footstepClip);
    }

    //public void PlayHurt()
    //{
    //    audioSource.PlayOneShot(hurtClip);
    //}

    // Podés seguir agregando funciones como PlayJump(), PlayAttack(), etc.
}
