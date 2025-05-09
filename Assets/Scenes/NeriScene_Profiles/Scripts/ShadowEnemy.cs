using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Enemies.States;
using Game.Mediators.Implementations;
using Game.Mediators.Interfaces;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShadowEnemy : BaseEnemy, IDetectableByPlayer
{
    public float appearRadius = 10f;
    public float disappearTime = 20f;
    public List<Transform> spawnPoints;

    [Header("Behavior Settings")]
    public float waitTimeBeforeAppear = 5f;
    public float lookThreshold = 5f;

    private float _lookTimer = 0f;
    private float _timeSinceLastSeen = 0f;
    private float _timeToReset = 1f;
    private bool _wasSeenThisFrame = false;
    
    public AudioClip appearSound;
    [HideInInspector] public AudioSource AudioSource;
    
    [Header("Death Settings")]
    public GameObject deathScreamerObject;
    public AudioClip deathSound;
    public float delayBeforeRestart = 2f;

    private AudioSource _audioSource;

    private EnvironmentMediator _environmentMediator;

    public IEnemyState CurrentState => _currentState;

    // Set up connections to the environment and mediator
    public void Configure(IEnemyMediator mediator, EnvironmentMediator env)
    {
        _enemyMediator = mediator;
        _environmentMediator = env;
    }

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();

        // Check if the enemy starts on the NavMesh
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.LogWarning("[ShadowEnemy] Not on the NavMesh at start.");
        }
    }

    protected override IEnemyState GetInitialState()
    {
        return new ShadowDormantState();
    }

    // Spawn near the player 
    public void AppearNearPlayer()
    {
        if (Target == null) return;

        Vector3 playerPosition = Target.position;
        Vector3 randomOffset = Random.insideUnitSphere * 5f;
        randomOffset.y = 0;

        Vector3 spawnPosition = playerPosition + randomOffset;

        // Warp to a valid NavMesh point near player
        if (NavMesh.SamplePosition(spawnPosition, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            Vector3 fixedPosition = hit.position;
            fixedPosition.y = 0.1f;

            m_Agent.Warp(fixedPosition);
            Appear();
        }
    }

    // Spawn behind or out of player's view
    public void AppearOutOfSight()
    {
        if (Target == null) return;

        int maxAttempts = 10;
        float spawnDistance = 6f;
        float minAngle = 100f;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomDir = Random.insideUnitSphere;
            randomDir.y = 0;
            randomDir.Normalize();

            Vector3 candidate = Target.position + randomDir * spawnDistance;
            Vector3 dirToCandidate = (candidate - Target.position).normalized;
            float angle = Vector3.Angle(Target.forward, dirToCandidate);

            // Try to spawn outside of player's view cone
            if (angle >= minAngle)
            {
                if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    m_Agent.Warp(hit.position);
                    Appear();
                    return;
                }
            }
        }

        // If no valid point, just appear anyway
        Appear();
    }

    public void Disappear()
    {
        Vanish();
    }

    // Called when the player sees the enemy
    public void OnSeenByPlayer()
    {
        _wasSeenThisFrame = true;
        _currentState?.OnSeenByPlayer(this);

        _lookTimer += Time.deltaTime;

        // Increase visual effects based on how long the player looks
        float intensity = Mathf.Clamp01(_lookTimer / lookThreshold);
        _environmentMediator?.ApplyVisualEffects(intensity);

        // Kill the player if they stare too long
        if (_lookTimer >= lookThreshold)
        {
            Debug.Log("[ShadowEnemy] Player stared too long. PLAYER DEAD.");
            StartCoroutine(HandlePlayerDeath());
            _lookTimer = 0f;
        }
    }

    public void ResetLookTimer()
    {
        _lookTimer = 0f;
        _environmentMediator?.ResetVisualEffects();
    }

    protected override void Update()
    {
        base.Update();

        // If not seen this frame, reset the timer gradually
        if (!_wasSeenThisFrame)
        {
            _timeSinceLastSeen += Time.deltaTime;
            if (_timeSinceLastSeen >= _timeToReset && _lookTimer > 0f)
            {
                ResetLookTimer();
            }
        }
        else
        {
            _timeSinceLastSeen = 0f;
        }

        _wasSeenThisFrame = false;
    }

    private void LateUpdate()
    {
        // Always look at the player while active
        if (Target != null && IsActive())
        {
            Vector3 lookPos = new Vector3(Target.position.x, transform.position.y, Target.position.z);
            transform.LookAt(lookPos);
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        if (deathSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(deathSound);
        }

  
        if (deathScreamerObject != null)
        {
            deathScreamerObject.SetActive(true);
        }

        yield return new WaitForSeconds(delayBeforeRestart);

        _environmentMediator?.ResetVisualEffects();

        Disappear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}