using Game.Enemies.States;
using UnityEngine;

/// <summary>
/// represents the Disappear state of the Shadow
/// The enemy hides for a period of time before returning to dormant
/// </summary>

public class ShadowDisappearState : IEnemyState
{
    private float _timer = 0f;
    private float _disappearDuration = 5f;

    public void EnterState(BaseEnemy enemy)
    {
        _timer = 0f;
        enemy.SetActiveVisualAndLogic(false);

        Debug.Log("[ShadowDisappearState] Shadow disappeared.");
    }

    public void UpdateState(BaseEnemy enemy)
    {
        _timer += Time.deltaTime;

        if (_timer >= _disappearDuration)
        {
            enemy.SwitchState(new ShadowDormantState());
        }
    }

    public void ExitState(BaseEnemy enemy)
    {
        Debug.Log("[ShadowDisappearState] Exiting Disappear state.");
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // disappear state does not react to being seen
    }
}