using Game.Enemies.States;
using UnityEngine;

// This state is for when the Shadow is hidden and inactive
public class ShadowDormantState : IEnemyState
{
    private float _timer = 0f; // Time counter

   
    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ShadowEnemy shadow) return;

        shadow.Agent.enabled = false; // Disable movement (NavMeshAgent)
        shadow.transform.position = new Vector3(5000f, 5000f, 5000f); // Move far away to hide
        Debug.Log("[ShadowDormantState] Moved far away and disabled NavMeshAgent.");
    }

   
    public void UpdateState(BaseEnemy enemy)
    {
        _timer += Time.deltaTime; // Count how much time has passed

        if (enemy is not ShadowEnemy shadow) return;

        // If enough time passed, switch to appear state
        if (_timer >= shadow.waitTimeBeforeAppear)
        {
            Debug.Log("[ShadowDormantState] Switching to AppearState.");
            enemy.SwitchState(new ShadowAppearState());
        }
    }

    public void ExitState(BaseEnemy enemy)
    {
        if (enemy is ShadowEnemy shadow)
        {
            shadow.Agent.enabled = true; // Enable movement again
        }

        Debug.Log("[ShadowDormantState] Exiting DORMANT STATE.");
    }
    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // No reaction when seen in Dormant
    }
}