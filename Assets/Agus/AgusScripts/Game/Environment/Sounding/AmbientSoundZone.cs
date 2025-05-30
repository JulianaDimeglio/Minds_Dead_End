using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class AmbientSoundZone : MonoBehaviour
{
    [Tooltip("Eventos de sonido que se reproducen aleatoriamente mientras el jugador esté en esta zona")]
    [SerializeField] private List<AmbientSoundEvent> soundEvents = new();

    private List<AudioSource> _audioSources = new();
    private List<Coroutine> _coroutines = new();
    private BoxCollider _zoneCollider;
    private bool _playerInside = false;

    private void Awake()
    {
        _zoneCollider = GetComponent<BoxCollider>();

        foreach (var evt in soundEvents)
        {
            GameObject audioSourceObj = new GameObject($"AudioSource_{evt.name}");
            audioSourceObj.transform.parent = this.transform; // como hijo de la zona
            audioSourceObj.transform.localPosition = Vector3.zero; // empieza centrado

            AudioSource source = audioSourceObj.AddComponent<AudioSource>();
            source.spatialBlend = 1f;
            source.playOnAwake = false;
            _audioSources.Add(source);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_playerInside)
        {
            _playerInside = true;

            for (int i = 0; i < soundEvents.Count; i++)
            {
                var routine = StartCoroutine(PlayAmbientLoop(soundEvents[i], _audioSources[i]));
                _coroutines.Add(routine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _playerInside)
        {
            _playerInside = false;

            foreach (var c in _coroutines)
                if (c != null) StopCoroutine(c);

            _coroutines.Clear();
        }
    }

    private IEnumerator PlayAmbientLoop(AmbientSoundEvent evt, AudioSource src)
    {
        while (_playerInside)
        {
            if (evt.clips.Count == 0)
            {
                Debug.LogWarning($"[AmbientZone] No hay clips en {evt.name}");
                yield break;
            }

            var clip = evt.clips[Random.Range(0, evt.clips.Count)];
            float waitTime = Random.Range(evt.minInterval, evt.maxInterval);

            src.pitch = Random.Range(evt.pitchMin, evt.pitchMax);
            src.volume = Random.Range(evt.volumeMin, evt.volumeMax);
            src.transform.position = GetRandomPositionInZone(_zoneCollider, evt);

            Debug.Log($"[AmbientZone] Reproduciendo '{clip.name}' con espera de {waitTime:F2} s");
            src.PlayOneShot(clip);

            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 GetRandomPositionInZone(BoxCollider box, AmbientSoundEvent evt)
    {
        Vector3 center = box.center + box.transform.position;
        Vector3 size = box.size;

        float x = Random.Range(-size.x / 2, size.x / 2);
        float y = evt.overrideYPosition ? evt.fixedY : Random.Range(-size.y / 2, size.y / 2);
        float z = Random.Range(-size.z / 2, size.z / 2);

        return center + new Vector3(x, y, z);
    }
}