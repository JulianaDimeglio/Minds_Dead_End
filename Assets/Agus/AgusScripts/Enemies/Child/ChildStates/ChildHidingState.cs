using Game.Enemies.States;
using UnityEngine;

public class ChildHidingState : IEnemyState
{
    public void EnterState(BaseEnemy enemy)
    {
        
    }
    public void UpdateState(BaseEnemy enemy)
    {
        if (enemy is not ChildEnemy child || child.WasFound) return;

        //Collider[] hits = Physics.OverlapSphere(child.transform.position, child.DetectionRadius, child.PlayerLayer);
        //if (hits.Length > 0)
        //{
        //    child.OnDiscovered();
        //}
    }
    public void ExitState(BaseEnemy enemy)
    {

    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        if (enemy is ChildEnemy child && !child.WasFound)
        {
            child.OnDiscovered();
        }
    }

}