using Game.Enemies.Mother;
using UnityEngine;

namespace Game.Mediators.Interfaces
{
    /// <summary>
    /// Interface that defines communication between enemies and other systems.
    /// It provides notifications for significant events and central control actions affecting enemy behavior.
    /// </summary>
    public interface IEnemyMediator
    {
        // Notifications triggered by enemy components

        /// <summary>
        /// Called when the child has been found by the player. Typically used to trigger the mother's hunt state.
        /// </summary>
        void NotifyChildFound();

        /// <summary>
        /// Called when a mirror or window has been broken (e.g. by the Mirror Lady after being ignored).
        /// </summary>
        void NotifyMirrorBroken();

        /// <summary>
        /// Called when an enemy performs a passive scare (e.g. visual or audio jump scare).
        /// </summary>
        /// <param name="enemy">The enemy that triggered the scare.</param>
        void NotifyEnemyTriggeredScare(BaseEnemy enemy);

        /// <summary>
        /// Called when the mother starts her hunt. This is typically triggered by the child being found.
        /// </summary>
        /// <param name="mother"></param>
        public void NotifyMotherHuntStarted(MotherEnemy mother);

        /// <summary>
        /// Called when the mother finishes her hunt.
        /// </summary>
        public void NotifyMotherHuntFinished(MotherEnemy mother);

        /// <summary>
        /// Called when an enemy has successfully executed the player.
        /// </summary>
        /// <param name="enemy">The enemy that killed the player.</param>
        void NotifyEnemyExecutedPlayer(BaseEnemy enemy);


        // Central control actions related to enemy state

        /// <summary>
        /// Activates selected enemies during the final loop of the game.
        /// </summary>
        void ActivateEnemiesForFinalLoop();

        /// <summary>
        /// Resets all enemies to their default or idle state.
        /// </summary>
        void ResetAllEnemies();
    }
}