using UnityEngine;

/// <summary>
/// Represents a breakable glass object, such as a mirror or window.
/// </summary>
public class BreakableGlass : MonoBehaviour
{
    [Tooltip("Unique ID used to trigger this glass from gameplay.")]
    public string Id;

    [Header("Effects")]
    [SerializeField] private GameObject shatteredVersion;
    [SerializeField] private AudioClip breakSound;

    private bool _isBroken = false;

    public void Shatter()
    {
        if (_isBroken) return;

        _isBroken = true;

        // Visual replacement
        if (shatteredVersion != null)
        {
            Instantiate(shatteredVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // Optional: audio
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }

        Debug.Log($"Glass object '{Id}' shattered.");
    }
}