using UnityEngine;

public class ChildTriggerJumpscare : MonoBehaviour
{
    [Header("Setup")]
    public GameObject childPrefab;             // Prefab with Animator
    public Transform spawnPoint;               // Start position
    public Transform destinationPoint;         // End position

    [Header("Settings")]
    public float speed = 3f;                   // Movement speed
    public AudioClip runSound;                 // Optional run sound

    private GameObject childInstance;
    private Animator anim;
    private AudioSource audioSource;
    private bool isRunning = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && childInstance == null)
        {
         

            // 1. Spawn and rotate child
            childInstance = Instantiate(childPrefab, spawnPoint.position, Quaternion.identity);

            // 2. Get Animator and play run animation
            anim = childInstance.GetComponent<Animator>();
            if (anim != null)
            {
                anim.applyRootMotion = false; // control movement manually
                anim.Play("Run");             // play animation directly by name
                Debug.Log("Animation 'Run' played");
            }

            // 3. Play sound
            audioSource = childInstance.AddComponent<AudioSource>();
            audioSource.clip = runSound;
            audioSource.playOnAwake = false;
            audioSource.Play();

            isRunning = true;
        }
    }

    void Update()
    {
        if (isRunning && childInstance != null)
        {
            // Move toward destination
            childInstance.transform.position = Vector3.MoveTowards(
                childInstance.transform.position,
                destinationPoint.position,
                speed * Time.deltaTime
            );

            // Check if arrived
            if (Vector3.Distance(childInstance.transform.position, destinationPoint.position) < 0.1f)
            {
                audioSource.Stop();
                isRunning = false;

                Destroy(childInstance, 0.5f);
                // deactivate this object
                Debug.Log("Child destroyed after reaching destination.");
                gameObject.SetActive(false);
            }
        }
    }
}