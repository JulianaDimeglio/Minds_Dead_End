using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip encounterSound;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayEncounter()
    {

        _audioSource.PlayOneShot(encounterSound);

    }
}
