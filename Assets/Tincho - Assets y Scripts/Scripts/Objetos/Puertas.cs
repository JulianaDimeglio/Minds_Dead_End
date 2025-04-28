using UnityEngine;

public class Puertas : MonoBehaviour, IInteraction
{
    [SerializeField] private bool _doorOpened;
    //public Animator _doorAnimation;
    public bool isInteracting = false;

    private void Start()
    {
        _doorOpened = false;
    }

    private void Update()
    {
        OpenDoor();
        //CloseDoor();
    }

    public void TriggerInteraction()
    {
        isInteracting = true;
        Debug.Log("Llega el trigger!");
    }

    private void OpenDoor()
    {
        if (!_doorOpened && isInteracting)
        {
            //_doorAnimation.SetBool("openedDoor", true);
            _doorOpened = true;
            print("Puerta abierta!");
        }
    }

    //private void CloseDoor()
    //{
    //    if (_doorOpened)
    //    {
    //        //_doorAnimation.SetBool("openedDoor", false);
    //        _doorOpened = false;
    //        print("Puerta cerrada!");
    //    }
    //}


}

