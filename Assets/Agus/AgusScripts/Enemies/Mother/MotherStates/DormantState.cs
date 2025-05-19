using Game.Enemies.States;
using UnityEngine;

namespace Game.Enemies.Mother.MotherStates
{
    /// <summary>
    /// Represents the dormant state of the Mother.
    /// In this state, the enemy is idle and invisible until triggered externally.
    /// </summary>
    public class DormantState : IEnemyState
    {
        public void EnterState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;

            mother.SetActiveVisualAndLogic(false); // Ensure she is not visible
            Debug.Log("[Mother] Entered DORMANT STATE.");
        }

        public void UpdateState(BaseEnemy enemy)
        {
            // The dormant state does nothing passively.
        }

        public void ExitState(BaseEnemy enemy)
        {
            Debug.Log("[Mother] Exited DORMANT STATE.");
        }

        public void OnSeenByPlayer(BaseEnemy enemy)
        {
            // The dormant state does not react to being seen.
        }
    }
}