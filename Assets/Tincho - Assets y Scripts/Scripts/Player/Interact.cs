using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Animator _animation;
    private PlayerMovement player;
    private bool _isInteracting;
    public bool hasInteractered;

    private void Start()
    {
        _animation = GetComponent<Animator>();   
        player = GetComponent<PlayerMovement>();
        _isInteracting = false;
        hasInteractered = false;
    }

    private void Update()
    {
        PlayerInteract();
    }

    private void PlayerInteract ()
    {
        if (Input.GetKeyDown(KeyCode.F) && !player.isMoving) 
        {
            _isInteracting = true;
            _animation.SetBool("Interacted", true);
            Debug.Log("Player is interacting.");
            hasInteractered = true; 
        }

        if (_isInteracting)
        {
            player.canMove = false;
        }
    }

    public void EndInteraction()
    {
        _animation.SetBool("Interacted", false);
        Debug.Log("Interaction animation ended.");
        _isInteracting = false;
        player.canMove = true;
        hasInteractered = false;
    }
}
