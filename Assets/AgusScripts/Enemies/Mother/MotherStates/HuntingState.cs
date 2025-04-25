using Game.Enemies.States;
using UnityEngine;
using Game.Enemies.Mother;
using Game.Mediators.Interfaces;

namespace Game.Enemies.Mother.MotherStates
{
    /// <summary>
    /// State in which the Mother actively hunts the player.
    /// Flickers environmental lights, moves toward the target,
    /// and kills the player if caught within the time limit.
    /// </summary>
    public class HuntingState : IEnemyState
    {
        public void EnterState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;

            mother.AppearOutsidePlayerView();
            mother.ResetHuntTimer();
            mother.Mediator?.NotifyMotherHuntStarted(mother);
            Debug.Log("[Mother] Entered HUNTING STATE.");
        }

        public void UpdateState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;


            // Flicker environment near the Mother


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

            // Stop flickering when hunt ends
            mother.Mediator?.NotifyMotherHuntFinished(mother);
            Debug.Log("[Mother] Exited HUNTING STATE.");
        }
    }
}