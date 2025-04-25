using Game.Mediators.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemies.Mother
{
    /// <summary>
    /// Represents the Mother enemy. Handles state transitions and hunting logic.
    /// Environmental effects are delegated to the EnvironmentMediator.
    /// </summary>
    
    public class MotherEnemy : BaseEnemy
    {
        [Header("Mother Settings")]
        [SerializeField] private float huntDuration = 10f;
        [SerializeField] private float speed = 2.5f;
        [SerializeField] private Transform player;

        private float _huntTimer;

        /// <summary>
        /// Reference to the player target.
        /// </summary>
        public Transform Target => player;

        /// <summary>
        /// Movement speed while hunting.
        /// </summary>
        public float Speed => speed;

        /// <summary>
        /// Called by the EnemyMediator when the child has been found.
        /// Triggers the Mother's hunting behavior.
        /// </summary>
        public void StartHunt()
        {
            SwitchState(new MotherStates.HuntingState());
        }



        /// <summary>
        /// Spawns the Mother outside of the player's view.
        /// </summary>
        public void AppearOutsidePlayerView()
        {
            Appear();
            Vector3 direction = Random.onUnitSphere;
            direction.y = 0f;

            Vector3 spawnOffset = direction.normalized * 10f;
            Vector3 spawnPosition = player.position + spawnOffset;
            spawnPosition.y = 2.71f;

            transform.position = spawnPosition;
        }

        /// <summary>
        /// Checks if the player is within execution range.
        /// </summary>
        public bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, player.position) < 3f;
        }

        /// <summary>
        /// Resets the internal hunting timer.
        /// </summary>
        public void ResetHuntTimer()
        {
            _huntTimer = 0f;
        }

        /// <summary>
        /// Advances the hunting timer and returns the elapsed time.
        /// </summary>
        public float AdvanceHuntTimer()
        {
            _huntTimer += Time.deltaTime;
            return _huntTimer;
        }

        /// <summary>
        /// Returns the max allowed hunting duration.
        /// </summary>
        public float GetHuntDuration()
        {
            return huntDuration;
        }
    }
}