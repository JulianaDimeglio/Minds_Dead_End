using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Enemies.States;

public class ShadowEnemy : BaseEnemy, IDetectableByPlayer
{
    public float appearRadius = 10f;
    public float disappearTime = 20f;
    public List<Transform> spawnPoints;

    [SerializeField] public float waitTimeBeforeAppear = 5f;

    private float _currentAppearTime = 0f;

    public IEnemyState CurrentState => _currentState;

    private void Awake()
    {
        m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Check if starting position is on the NavMesh
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.LogWarning("[ShadowEnemy] Not on the NavMesh at start.");
        }
    }

    protected override IEnemyState GetInitialState()
    {
        return new ShadowDormantState();
    }

    /// <summary>
    /// Moves the Shadow near the player using a valid NavMesh position.
    /// </summary>
    public void AppearNearPlayer()
    {
        Vector3 playerPosition = Target.position;

        Vector3 randomOffset = Random.insideUnitSphere * 5f;
        randomOffset.y = 0;

        Vector3 spawnPosition = playerPosition + randomOffset;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 5f, NavMesh.AllAreas))
        {
            Vector3 fixedPosition = hit.position;
            fixedPosition.y = 0.1f; // Slightly raise to avoid floor clipping

            m_Agent.Warp(fixedPosition);
            Appear();
            Debug.Log("[ShadowEnemy] Appeared near the player at: " + fixedPosition);
        }
        else
        {
            Debug.LogWarning("[ShadowEnemy] No valid position found near the player.");
        }
    }

    /// <summary>
    /// Hides the Shadow.
    /// </summary>
    public void Disappear()
    {
        Vanish();
    }

    /// <summary>
    /// Called when the player sees the Shadow.
    /// </summary>
    public void OnSeenByPlayer()
    {
        _currentState?.OnSeenByPlayer(this);
    }

    private void LateUpdate()
    {
        // Always rotate to face the player
        if (Target != null && IsActive())
        {
            Vector3 lookPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);
            transform.LookAt(lookPosition);
        }
    }
}