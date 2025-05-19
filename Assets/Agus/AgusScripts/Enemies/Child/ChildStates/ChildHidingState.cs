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

        Debug.Log("[ChildHidingState] El niño fue visto. ¡Activando transición a FoundState!");

        child.MarkAsFound(); // separa esta lógica del estado

        child.Mediator?.NotifyChildFound();
        child.SwitchState(new ChildFoundState());
    }
}