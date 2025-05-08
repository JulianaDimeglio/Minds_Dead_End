using Game.Enemies.States;
using Game.Mediators.Implementations;
using Game.Mediators.Interfaces;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Abstract base class for all enemy types.
/// Provides common functionality such as state management, movement, visibility, and mediator communication.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected IEnemyMediator _enemyMediator;
    protected IEnemyState _currentState;
    protected bool canChangeState = false;
    protected NavMeshAgent m_Agent;
    protected float m_Distance;

    /// <summary>
    /// Injects the enemy mediator dependency.
    /// This should be called during initialization by an external controller.
    /// </summary>
    /// <param name="mediator">The central enemy mediator.</param>
    public virtual void Configure(IEnemyMediator mediator)
    {
        _enemyMediator = mediator;
    }

    public IEnemyMediator Mediator => _enemyMediator;

    /// <summary>
    /// Switches the enemy's active state using the State pattern.
    /// </summary>
    /// <param name="newState">The new state to enter.</param>
    public virtual void SwitchState(IEnemyState newState)
    {
        if (newState == null || newState == _currentState) return;

        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    /// <summary>
    /// Unity Update loop, delegated to the current state.
    /// </summary>
    protected virtual void Update()
    {
        _currentState?.UpdateState(this);
    }



    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.LogWarning($"{name} is not on the NavMesh!");
        }
    }

    /// <summary>
    /// Executes the player. Can be overridden for custom execution behavior.
    /// </summary>
    public virtual void KillPlayer()
    {
        Debug.Log($"{name} executed the player.");
        _enemyMediator?.NotifyEnemyExecutedPlayer(this);
        // TODO: trigger kill animation, sound, or cinematic
    }

    /// <summary>
    /// Makes the enemy appear visually (without disabling logic).
    /// </summary>
    public virtual void Appear()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the enemy visually (without destroying the object or stopping logic).
    /// </summary>
    public virtual void Vanish()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if the enemy is currently active in the scene.
    /// </summary>
    public virtual bool IsActive()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// Moves the enemy toward a target position at a specified speed.
    /// </summary>
    /// <param name="target">The transform to move toward.</param>
    /// <param name="speed">The movement speed.</param>
    public void MoveTowards(Transform target, float speed)
    {
        /*
        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        */
        m_Distance = Vector3.Distance(m_Agent.transform.position, target.position);

        if (m_Distance > 0.5f)
        {
            m_Agent.speed = speed;
            m_Agent.SetDestination(target.position);
        }
    }

    /// <summary>
    /// Returns whether the mediator was assigned properly.
    /// </summary>
    public bool IsMediatorAssigned => _enemyMediator != null;
}
