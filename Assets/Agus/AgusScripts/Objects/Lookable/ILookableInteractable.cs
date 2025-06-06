using UnityEngine;
using System.Collections;

public interface ILookableInteractable
{
    Transform GetFocusTarget();
    IEnumerator PlayInteraction(System.Action onComplete);
}