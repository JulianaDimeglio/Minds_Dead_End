using Game.Enemies.States;
using UnityEngine;

/// <summary>
/// represents the dormant state of the Shadow
/// the enemy stays hidden and waits before appearing
/// </summary>
public class ShadowDormantState : IEnemyState
{
    private float _timer = 0f;

    public void EnterState(BaseEnemy enemy)
    {
        _timer = 0f;

        // move the Shadow far away to hide it
        enemy.transform.position = new Vector3(9999f, 9999f, 9999f);

        Debug.Log("[ShadowDormantState] Entered Dormant and moved out of the map");
    }

    public void UpdateState(BaseEnemy enemy)
    {
        _timer += Time.deltaTime;

        ShadowEnemy shadow = (ShadowEnemy)enemy;

        if (_timer >= shadow.waitTimeBeforeAppear)
        {
            enemy.SwitchState(new ShadowAppearState());
        }
    }
    public void ExitState(BaseEnemy enemy)
    {
        Debug.Log("[ShadowDormantState] Exiting Dormant.");
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // Dormant state does not react to being seen
    }
}
