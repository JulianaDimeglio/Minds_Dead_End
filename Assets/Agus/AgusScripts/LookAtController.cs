using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LookAtController : MonoBehaviour
{
    public Transform objectToLookAt;
    public float headWeight;
    public float bodyWeight;
    public Animator animator;

    void Start()
    {
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (objectToLookAt != null)
        {
            animator.SetLookAtPosition(objectToLookAt.position);
            animator.SetLookAtWeight(headWeight, bodyWeight);
        }
    }
}
