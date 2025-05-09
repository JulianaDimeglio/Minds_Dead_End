using UnityEngine;
using System.Collections;
using Game.Enemies.States;

public class ChildAwaitingAppearanceState : IEnemyState
{
    private Coroutine _waitCoroutine;

    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child) return;

        Debug.Log("[ChildAwaitingAppearanceState] El niño está esperando para aparecer.");
        _waitCoroutine = child.StartCoroutine(WaitAndSwitchToHiding(child, child.AppearFrecuency));
    }

    public void ExitState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child) return;

        if (_waitCoroutine != null)
        {
            child.StopCoroutine(_waitCoroutine);
            _waitCoroutine = null;
        }
    }

    public void UpdateState(BaseEnemy enemy) { }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // No hace nada aún
    }

    private IEnumerator WaitAndSwitchToHiding(ChildEnemy child, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("[ChildAwaitingAppearanceState] Apareciendo en el mundo.");
        child.SwitchState(new ChildHidingState());
    }
}