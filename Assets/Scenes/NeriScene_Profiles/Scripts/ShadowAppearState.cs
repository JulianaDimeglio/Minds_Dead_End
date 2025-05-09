using Game.Enemies.States;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// represents the Appear state of the Shadow
/// the enemy becomes visible and can be seen by the player
/// </summary>
public class ShadowAppearState : IEnemyState
{
    private float _lookTimer = 0f;
    private float _appearDuration = 20f;

    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ShadowEnemy shadow) return;

        shadow.Agent.enabled = true; // Reactivar el NavMeshAgent
        shadow.AppearNearPlayer();
        Debug.Log("[ShadowAppearState] Reappeared near the player.");
    }

    public void UpdateState(BaseEnemy enemy)
    {
        _lookTimer += Time.deltaTime;

        if (_lookTimer >= _appearDuration)
        {
            enemy.SwitchState(new ShadowDormantState());
        }
    }

    public void ExitState(BaseEnemy enemy)
    {
        Debug.Log("[ShadowAppearState] Exiting AppearState.");
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // Logic for when the player sees the Shadow can be added here
    }
}