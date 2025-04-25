using Game.Enemies.Mother.MotherStates;
using Game.Enemies.States;
using Game.Mediators.Interfaces;
using System.Collections;
using System.Linq;
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

        [Header("Spawn Settings")]
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float minimumDistanceFromPlayer = 10f;

        [Header("Mother Settings")]
        [SerializeField] private float huntDuration = 10f;
        [SerializeField] private float speed = 2.5f;
        [SerializeField] private Transform player;
        public float flickRadius = 7.5f;



        private float _huntTimer;
        public bool canHunt = false;

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


        public IEnemyState CurrentState => _currentState;


        /// <summary>
        /// Spawns the Mother outside of the player's view.
        /// </summary>
        public void AppearOutsidePlayerView()
        {
            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                Debug.LogWarning("[MotherEnemy] No spawn points assigned.");
                return;
            }
            Vector3 playerPosition = player.position;
            var validSpawns = spawnPoints
                .Where(spawn => Vector3.Distance(spawn.position, playerPosition) >= minimumDistanceFromPlayer)
                .ToArray();

            if (validSpawns.Length == 0)
            {
                Debug.LogWarning("[MotherEnemy] No valid spawn points found outside player view.");
                return;
            }
            Transform selectedSpawn = validSpawns[Random.Range(0, validSpawns.Length)];
            Appear();
            m_Agent.Warp(selectedSpawn.position);
            Debug.Log($"[MotherEnemy] Spawned at {selectedSpawn.name} ({selectedSpawn.position}).");
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