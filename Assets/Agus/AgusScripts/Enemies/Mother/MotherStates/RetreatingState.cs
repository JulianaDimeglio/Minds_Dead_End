using Game.Enemies.States;
using UnityEngine;

namespace Game.Enemies.Mother.MotherStates
{
    /// <summary>
    /// Represents the state where the Mother retreats after failing to catch the player.
    /// Can trigger visual/audio effects and disappears after a short delay.
    /// </summary>
    public class RetreatingState : IEnemyState
    {
        private float _timer = 0f;
        private float _retreatDuration = 2.5f;

        public void EnterState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;

            _timer = 0f;

            // Optional: play a retreat animation or sound here in the future
            Debug.Log("[Mother] Entered RETREATING STATE.");
        }

        public void UpdateState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;

            _timer += Time.deltaTime;

            if (_timer >= _retreatDuration)
            {
                // Disappear and return to passive
                mother.Vanish();
                mother.SwitchState(new DormantState()); // Or any idle/passive state you define

                Debug.Log("[Mother] Retreat complete. Returning to passive state.");
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy) return;

            // Optional: trigger ambient recovery, silence, etc.
            Debug.Log("[Mother] Exited RETREATING STATE.");
        }

        public void OnSeenByPlayer(BaseEnemy enemy)
        {
            // The dormant state does not react to being seen.
        }
    }
}