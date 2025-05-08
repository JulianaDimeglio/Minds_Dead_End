using UnityEngine;

public class Interact : MonoBehaviour
{
    //private Animator _animation;
    private PlayerMovement player;
    //private bool _isInteracting;

    [Header("Raycast Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    [SerializeField] private AudioSource _interactSFX;

    private void Start()
    {
        //_animation = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        //_isInteracting = false;
    }

    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactDistance, Color.green);
        PlayerInteract();
    }

    private void PlayerInteract()
    {
        if (Input.GetKeyDown(KeyCode.F)/* && !player.isMoving*/)
        {
              
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {
                _interactSFX.Play();
                //_isInteracting = true;
                //_animation.SetBool("Interacted", true);
                Debug.Log("Player is interacting with " + hit.collider.name);

                IInteraction interactable = hit.collider.GetComponent<IInteraction>();
                if (interactable != null)
                {
                    interactable.TriggerInteraction();
                }
            }
        }

        //if (_isInteracting)
        //{
        //    player.canMove = false;
        //}
    }

    //public void EndInteraction()
    //{
    //    _animation.SetBool("Interacted", false);
    //    Debug.Log("Interaction animation ended.");
    //    _isInteracting = false;
    //    player.canMove = true;
    //}
}
