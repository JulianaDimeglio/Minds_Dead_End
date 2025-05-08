using Game.Enemies.States;
using UnityEngine;
using Game.Enemies.Mother.MotherStates;

namespace Game.Enemies.SharedStates
{
    /// <summary>
    /// Represents a passive scare: a non-lethal appearance that creates fear through ambient effects.
    /// The scare is triggered via the EnemyMediator, which then coordinates any environmental responses.
    /// </summary>
    public class PassiveScareState : IEnemyState
    {
        private float _timer = 0f;
        private float _duration = 2f;

        public void EnterState(BaseEnemy enemy)
        {
            enemy.Appear();

            enemy.Mediator?.NotifyEnemyTriggeredScare(enemy);

            _timer = 0f;

            Debug.Log($"[{enemy.name}] Entered PASSIVE SCARE STATE.");
        }

        public void UpdateState(BaseEnemy enemy)
        {
            _timer += Time.deltaTime;

            if (_timer >= _duration)
            {
                enemy.Vanish();
                enemy.SwitchState(new DormantState()); // Or any other idle state
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            Debug.Log($"[{enemy.name}] Exited PASSIVE SCARE STATE.");
        }

        public void OnSeenByPlayer(BaseEnemy enemy)
        {
            // The dormant state does not react to being seen.
        }
    }
}