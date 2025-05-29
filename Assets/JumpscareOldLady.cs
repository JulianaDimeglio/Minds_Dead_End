using UnityEngine;

public class JumpscareOldLady : MonoBehaviour
{
    [Header("Setup")]
    public GameObject edithPrefab;             // Prefab already in scene, initially deactivated
    public Transform spawnPoint;               // Start position
    public Transform destinationPoint;         // End position

    [Header("Settings")]
    public float speed = 3f;                   // Movement speed
    public AudioClip runSound;                 // Optional run sound

    private GameObject edithInstance;
    private Animator anim;
    private AudioSource audioSource;
    private bool isRunning = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && edithInstance == null)
        {
            // 1. Get reference to existing (disabled) prefab
            edithInstance = edithPrefab;
            edithInstance.transform.position = destinationPoint.position;
            edithInstance.SetActive(true);

            // 2. Animator setup
            anim = edithInstance.GetComponent<Animator>();
            if (anim != null)
            {
                anim.applyRootMotion = false;
                anim.Play("Demon_FloorCrawl_Forward");
                Debug.Log("Animation 'Run' played");
            }

            // 3. Play sound
            audioSource = edithInstance.GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = edithInstance.AddComponent<AudioSource>();

            audioSource.clip = runSound;
            audioSource.playOnAwake = false;
            audioSource.Play();

            isRunning = true;
        }
    }

    void Update()
    {
        if (isRunning && edithInstance != null)
        {
            Vector3 direction = spawnPoint.position - edithInstance.transform.position;
            direction.y = 0; // Flatten to horizontal plane

            if (direction != Vector3.zero)
            {
                edithInstance.transform.rotation = Quaternion.LookRotation(direction);
            }

            // Move toward destination
            edithInstance.transform.position = Vector3.MoveTowards(
                edithInstance.transform.position,
                spawnPoint.position,
                speed * Time.deltaTime
            );

            // Check if arrived
            if (Vector3.Distance(edithInstance.transform.position, spawnPoint.position) < 0.1f)
            {
                audioSource.Stop();
                isRunning = false;

                Destroy(edithInstance, 0.5f);
                Debug.Log("Old lady destroyed after reaching destination.");
                gameObject.SetActive(false);
            }
        }
    }
}