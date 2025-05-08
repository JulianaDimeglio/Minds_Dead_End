using System.Collections;
using UnityEngine;
using Game.Enemies.States;


namespace Game.Enemies.Mother.MotherStates
{
    public class KillState : IEnemyState
    {
        public void EnterState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;

            Debug.Log("[Mother] Entered KILLING STATE.");
            //mother.PlayKillAnimation();
            mother.Mediator?.NotifyEnemyExecutedPlayer(mother);

            // Desactivar luces, sonidos, input...
            mother.StartCoroutine(ExecuteKillSequence(mother));
        }

        public void UpdateState(BaseEnemy enemy)
        {
            // No hay lógica en esta fase
        }

        public void ExitState(BaseEnemy enemy)
        {
            if (enemy is not MotherEnemy mother) return;
            //mother.StopKillAnimation();
            Debug.Log("[Mother] Exited KILLING STATE.");
        }

        private IEnumerator ExecuteKillSequence(MotherEnemy mother)
        {
            yield return new WaitForSeconds(2.5f); // Wait for animation

            //GameManager.Instance.TriggerGameOver();
            mother.SwitchState(new DormantState()); // destroys itself
        }

        public void OnSeenByPlayer(BaseEnemy enemy)
        {
            // The dormant state does not react to being seen.
        }
    }
}