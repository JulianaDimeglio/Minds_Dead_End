using Game.Enemies.States;
using UnityEngine;

public class ChildFoundState : IEnemyState
{
    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child) return;

        child.stopAgent();

        Debug.Log("[ChildFoundState] Child has been discovered.");

        // reproducir animación o sonido
        //Animator animator = child.GetComponent<Animator>();
        //if (animator != null)
        //{
        //    animator.SetTrigger("Found"); // necesitás tener este trigger en el Animator Controller
        //}

        // Opcional: desaparecer visualmente tras un delay
        child.StartCoroutine(DisappearAfterSeconds(child, 2f));
    }
    public void UpdateState(BaseEnemy enemy)
    {
        // En este estado, no hace nada activamente
    }

    public void ExitState(BaseEnemy enemy)
    {
        // Nada por ahora
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        
    }

    private System.Collections.IEnumerator DisappearAfterSeconds(ChildEnemy child, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Desaparecer visualmente
        child.SetActiveVisualAndLogic(false);

        // Notificar a la madre a través del mediator
        child.Mediator?.NotifyChildFound();

        Debug.Log("[ChildFoundState] Child has vanished and notified the mother.");
    }
}