using UnityEngine;
using Game.Mediators.Interfaces;
using Game.Enemies.Mother.MotherStates;
using Game.Enemies.Mother;

namespace Game.Mediators.Implementations
{
    /// <summary>
    /// Handles coordination between enemy components and other systems.
    /// Routes enemy events to gameplay and environmental responses.
    /// </summary>
    public class EnemyMediator : MonoBehaviour, IEnemyMediator
    {
        [Header("Enemy References")]
        [SerializeField] private MotherEnemy mother;
        //[SerializeField] private ShadowEnemy shadow;
        //[SerializeField] private MirrorLadyEnemy mirrorLady;
        [SerializeField] private ChildEnemy child;

        [Header("Environment Mediator")]
        [SerializeField] private EnvironmentMediator environmentMediator;

        // ------------------------
        // Gameplay Coordination
        // ------------------------

        void Start()
        {
            mother.Configure(this);
            child.Configure(this);
        }

        public void NotifyChildFound()
        {
            Debug.Log("Child found — triggering Mother hunt.");
            mother?.StartHunt();
        }

        public void NotifyMirrorBroken()
        {
            Debug.Log("Mirror broken — triggering environment.");
            environmentMediator?.ShatterGlass("mirror_1"); // example ID
        }

        public void NotifyEnemyTriggeredScare(BaseEnemy enemy)
        {
            Debug.Log($"Enemy {enemy.name} triggered a passive scare.");

            if (environmentMediator == null) return;

            // Example: flicker lights, play a sound, trigger effect
            environmentMediator.StartFlickerNear(enemy.transform.position, 6f);
            environmentMediator.PlayAmbientSound("scare_static");

            // Optional: trigger specific effect per enemy
            if (enemy.name.Contains("Mirror"))
            {
                environmentMediator.ShatterGlass("window_backroom");
            }
        }

        public void NotifyMotherHuntStarted(MotherEnemy mother)
        {
            Debug.Log("[Mediator] Mother hunt started.");
            environmentMediator?.TriggerMotherEntrance(mother.transform, mother.flickRadius);
        }

        public void NotifyMotherHuntFinished(MotherEnemy mother)
        {
            Debug.Log("[Mediator] Mother hunt finished.");

            environmentMediator?.TriggerMotherOut();
            child.StartHiding();
            //environmentMediator?.ResetAmbientSound("scare_static");
            //environmentMediator?.CloseDoor("living_room_door");
        }

        public void NotifyEnemyExecutedPlayer(BaseEnemy enemy)
        {
            Debug.Log($"Enemy {enemy.name} executed the player.");
            environmentMediator?.TriggerMotherOut();
            // TODO: show death screen, block input, etc.
        }

        // ------------------------
        // Centralized Enemy Control
        // ------------------------

        public void ActivateEnemiesForFinalLoop()
        {
            Debug.Log("Activating enemies for the final loop.");

            // Example logic
            mother?.SetActiveVisualAndLogic(true);
            //shadow?.Appear();
        }

        public void ResetAllEnemies()
        {
            mother?.SwitchState(new DormantState());
            //shadow?.SwitchState(new HiddenState());
            //mirrorLady?.SwitchState(new WaitingOutsideState());
            //child?.SwitchState(new HidingState());
        }
    }
}