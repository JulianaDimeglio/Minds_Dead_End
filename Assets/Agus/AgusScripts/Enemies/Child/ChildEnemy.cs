using UnityEngine;
using System.Collections.Generic;
using Game.Enemies.States;

public class ChildEnemy : BaseEnemy, IDetectableByPlayer
{
    [SerializeField] private List<Transform> hidingSpots;
    [SerializeField] private float detectionRadius = 2f;

    public Transform CurrentHidingSpot { get; private set; }
    public float DetectionRadius => detectionRadius;
    public bool WasFound { get; private set; } = false;
    public IEnemyState CurrentState => _currentState;

    private void Awake()
    {
        m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    protected override IEnemyState GetInitialState()
    {
        return new ChildHidingState();
    }

    private void HideInRandomSpot()
    {
        if (hidingSpots == null || hidingSpots.Count == 0)
        {
            Debug.LogWarning("[ChildEnemy] No hiding spots assigned.");
            return;
        }

        int index = Random.Range(0, hidingSpots.Count);
        CurrentHidingSpot = hidingSpots[index];
        transform.position = CurrentHidingSpot.position;

    }

    public void OnDiscovered()
    {
        if (WasFound) return;

        WasFound = true;
        Debug.Log("[ChildEnemy] I was found!");
        SwitchState(new ChildFoundState());
        // Cambiar estado si se desea, por ejemplo:
        // SwitchState(new ChildFoundState());
    }

    public void OnSeenByPlayer()
    {
        _currentState.OnSeenByPlayer(this);
    }
}
