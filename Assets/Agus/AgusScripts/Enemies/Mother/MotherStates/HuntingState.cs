using Game.Enemies.States;
using UnityEngine;
using System.Collections;

namespace Game.Enemies.Mother.MotherStates
{
    /// <summary>
    /// State in which the Mother actively hunts the player.
    /// Flickers environmental lights, moves toward the target,
    /// and kills the player if caught within the time limit.
    /// </summary>
    public class HuntingState : IEnemyState
    {
        private IEnumerator BeginHuntAfterDelay(MotherEnemy mother, float delay)
        {
            yield return new WaitForSeconds(delay);
            mother.canHunt = true;
        }
        public void EnterState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;
            mother.canHunt = false;
            Debug.Log("[mother] Entering HUNTING STATE.");

            mother.Mediator?.NotifyMotherHuntStarted(mother);
            mother.AppearOutsidePlayerView();
            mother.ResetHuntTimer();
            mother.StartCoroutine(BeginHuntAfterDelay(mother, 3f));
            Debug.Log("[Mother] aaaaaaaaaaaasd HUNTING STATE.");
        }

        public void UpdateState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;
            if (!mother.canHunt) return;
            // Move toward player
            mother.MoveTowards(mother.Target, mother.Speed);

            // Check for execution
            if (mother.IsPlayerInRange())
            {
                mother.SwitchState(new KillState()); // Or any idle/passive state you define
                mother.KillPlayer();
                return;
            }
            
            // Check for timeout
            float elapsed = mother.AdvanceHuntTimer();
            if (elapsed >= mother.GetHuntDuration())
            {
                Debug.Log("[Mother] Hunt failed — retreating.");
                mother.SwitchState(new RetreatingState()); // Ensure this state exists
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;
            mother.canHunt = false;
            mother.stopAgent();
            // Stop flickering when hunt ends
            mother.Mediator?.NotifyMotherHuntFinished(mother);
            Debug.Log("[Mother] Exited HUNTING STATE.");
        }

        public void OnSeenByPlayer(BaseEnemy enemy)
        {
            // The dormant state does not react to being seen.
        }
    }
}