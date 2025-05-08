using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    private PlayerMovement player;
    private Interact playerState;
    [SerializeField] private bool _doorOpened;
    public Animator _doorAnimation;

    private void Start()
    {
        _doorOpened = false;
    }

    private void Update()
    {
        //CloseDoor();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovement>();
            playerState = other.GetComponent<Interact>();

            if (player != null && playerState.hasInteractered)
            {
                //Debug.Log("Jugador detectado");

                if (!_doorOpened)
                {
                    _doorAnimation.SetBool("openedDoor", true);
                    _doorOpened = true;
                    print("Puerta abierta!");
                }
            }
        }
    }

    //private void CloseDoor()
    //{
    //    if (_doorOpened)
    //    {
    //        _doorAnimation.SetBool("openedDoor", false);
    //        _doorOpened = false;
    //        print("Puerta cerrada!");
    //    }
    //}


}

