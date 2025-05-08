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
        _lookTimer = 0f;

        ShadowEnemy shadow = (ShadowEnemy)enemy;
        shadow.AppearNearPlayer();

        Debug.Log("[ShadowAppearState] Appeared near the player.");
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