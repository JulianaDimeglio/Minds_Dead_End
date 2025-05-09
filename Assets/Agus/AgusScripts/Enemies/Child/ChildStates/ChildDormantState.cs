using Game.Enemies.States;
using System.Collections;
using UnityEngine;

public class ChildDormantState : IEnemyState
{
    private Coroutine _waitCoroutine;
    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child) return;

        Debug.Log("[ChildDormantState] The child is dormant.");

        child.Agent.isStopped = true;

        child.gameObject.SetActive(true);
        child.GetComponent<Renderer>().enabled = false;

        var collider = child.GetComponent<Collider>();
        if (collider != null) collider.enabled = false;
    }

    public void ExitState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child) return;

        if (_waitCoroutine != null)
        {
            child.StopCoroutine(_waitCoroutine);
            _waitCoroutine = null;
        }

        Debug.Log("[ChildDormantState] Exiting dormant state.");

        child.GetComponent<Renderer>().enabled = true;
        var collider = child.GetComponent<Collider>();
        if (collider != null) collider.enabled = true;
    }

    public void UpdateState(BaseEnemy enemy)
    {
        // No hace nada en estado dormido
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // No debería poder ser visto en este estado
        Debug.LogWarning("[ChildDormantState] Child was seen when being in dormant (is it well configured)?");
    }
}