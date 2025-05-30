using System.Collections;
using UnityEngine;

public class AmbientSoundEmitter : MonoBehaviour
{
    [SerializeField] private AmbientSoundEvent soundEvent;
    [SerializeField] private bool playOnAwake = false;

    private AudioSource _audioSource;
    private Coroutine _loopRoutine;
    private bool _isActive = false;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.spatialBlend = 1f;
        _audioSource.playOnAwake = false;

        if (playOnAwake)
            ActivateEmitter();
    }

    public void ActivateEmitter()
    {
        if (_isActive) return;
        _isActive = true;
        _loopRoutine = StartCoroutine(PlayLoop());
    }

    public void DeactivateEmitter()
    {
        if (!_isActive) return;
        _isActive = false;
        if (_loopRoutine != null)
            StopCoroutine(_loopRoutine);
    }

    private IEnumerator PlayLoop()
    {
        while (_isActive)
        {
            if (soundEvent.clips.Count == 0) yield break;

            var clip = soundEvent.clips[Random.Range(0, soundEvent.clips.Count)];
            _audioSource.pitch = Random.Range(soundEvent.pitchMin, soundEvent.pitchMax);
            _audioSource.volume = Random.Range(soundEvent.volumeMin, soundEvent.volumeMax);
            _audioSource.PlayOneShot(clip);

            float delay = Random.Range(soundEvent.minInterval, soundEvent.maxInterval);
            yield return new WaitForSeconds(delay);
        }
    }
}