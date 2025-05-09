using Game.Enemies.States;
using UnityEngine;

public class ChildHidingState : IEnemyState
{
    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child) return;

        child.SetActiveVisualAndLogic(true);
        child.PrepareToHide();
    }

    public void ExitState(BaseEnemy enemy)
    {
        // Nada por ahora
    }

    public void UpdateState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child || child.WasFound) return;

        child.CheckVisibilityAndMaybeRelocate();
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child || child.WasFound) return;

        Debug.Log("[ChildHidingState] El ni�o fue visto. �Activando transici�n a FoundState!");

        child.MarkAsFound(); // separa esta l�gica del estado

        child.Mediator?.NotifyChildFound();
        child.SwitchState(new ChildFoundState());
    }
}