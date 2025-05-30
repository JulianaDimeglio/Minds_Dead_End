using Game.Enemies.States;
using UnityEngine;
using UnityEngine.Rendering;

public class ShadowAppearState : IEnemyState
{
    private float _lookTimer = 0f;
    private float _appearDuration = 20f;

    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ShadowEnemy shadow) return;

        shadow.Agent.enabled = true;

        // Appear somewhere the player can’t see
        shadow.AppearOutOfSight();

        // Play sound when he appears
        if (shadow.appearSound != null && shadow.AudioSource != null)
        {
            shadow.AudioSource.PlayOneShot(shadow.appearSound);
        }

        Debug.Log("[ShadowAppearState] Reappeared out of player’s view.");
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
        Debug.Log("SEEEN BY PLAYA");
        // get component ShadowSounds of enemy
        var shadowSounds = enemy.GetComponent<ShadowSounds>();
        shadowSounds?.PlayEncounter();
        enemy.SwitchState(new ShadowDormantState());
        // Logic for when the player sees the Shadow can be added here
    }
}