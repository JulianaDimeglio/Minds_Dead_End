using UnityEngine;

public class MovableObject : MonoBehaviour, IInteraction
{
    private bool isBeingHeld = false;
    private Transform player;
    private Rigidbody rb;
    [SerializeField]
    public float objectWeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isBeingHeld && Input.GetKeyDown(KeyCode.G))
        {
            ReleaseObject();
        }
    }

    public void TriggerInteraction()
    {
        if (CompareTag("Grabbable"))
        {
            player = GameObject.FindWithTag("Player").transform;
            // toggle between the states of the object 
            isBeingHeld = !isBeingHeld;

            //if the object is being grabbed, call GrabObject, otherwise release it 
            if (isBeingHeld)
            {
                GrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }
    }

    private void GrabObject()
    {
        //set the object as a child of the player so it moves with him 
        transform.SetParent(player);
        //position the object in front of the player 
        transform.localPosition = new Vector3(0, 1.5f, 2f); 
        rb.isKinematic = true;
    }

    private void ReleaseObject()
    {
        //makes the object not a child of the player again 
        transform.SetParent(null);
        rb.isKinematic = false;
    }
}
