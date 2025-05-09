using UnityEngine;
using System.Collections.Generic;
using Game.Enemies.States;
using Game.Mediators.Interfaces;
using Game.Enemies.Mother.MotherStates;

public class ChildEnemy : BaseEnemy, IDetectableByPlayer
{
    [Header("Child Settings")]
    [SerializeField] private List<Transform> hidingSpots;
    [SerializeField] private float timeBetweenMoves = 20f;
    [SerializeField] private float visibilityCheckInterval = 0.3f;
    [SerializeField] private float _appearFrecuency = 10f;
    [SerializeField] private bool isChildIteration = true;
    private float _nextMoveTime;
    private float _nextVisibilityCheckTime;

    private Camera _camera;
    private Collider _collider;
    private bool _wasFound = false;

    public bool WasFound => _wasFound;
    public float AppearFrecuency => _appearFrecuency;

    private void Awake()
    {
        _camera = Camera.main;
        _collider = GetComponent<Collider>();
    }

    protected override IEnemyState GetInitialState()
    {
        if (isChildIteration)
        {
            return new ChildAwaitingAppearanceState();
        }
        else
        {
            return new ChildDormantState();
        }
    }
    public void PrepareToHide()
    {
        
        HideInRandomSpot();
        _nextMoveTime = Time.time + timeBetweenMoves;
        _nextVisibilityCheckTime = Time.time + visibilityCheckInterval;
    }

    public void CheckVisibilityAndMaybeRelocate()
    {
        if (Time.time < _nextVisibilityCheckTime) return;
        _nextVisibilityCheckTime = Time.time + visibilityCheckInterval;

        if (!IsVisibleToCamera())
        {
            if (Time.time >= _nextMoveTime)
            {
                HideInRandomSpot();
                _nextMoveTime = Time.time + timeBetweenMoves;
            }
        }
        else
        {
            _nextMoveTime = Time.time + timeBetweenMoves; // reinicia el contador si se lo ve
        }
    }

    private void HideInRandomSpot()
    {
        if (hidingSpots == null || hidingSpots.Count == 0) return;

        int index = Random.Range(0, hidingSpots.Count);
        Transform spot = hidingSpots[index];
        transform.position = spot.position;
        Debug.Log($"El niño ha aparecido en un lugar escondido. {spot.name}");
        // Animaciones o efectos si querés
    }

    private bool IsVisibleToCamera()
    {
        if (_camera == null || _collider == null) return false;

        Vector3 point = _collider.bounds.center;
        Vector3 viewport = _camera.WorldToViewportPoint(point);

        bool inFrustum = viewport.z > 0 &&
                         viewport.x > 0 && viewport.x < 1 &&
                         viewport.y > 0 && viewport.y < 1;

        if (!inFrustum) return false;

        // Línea de visión directa sin obstrucción
        if (Physics.Linecast(_camera.transform.position, point, out RaycastHit hit))
        {
            return hit.collider == _collider || hit.collider.transform.IsChildOf(transform);
        }

        return false;
    }

    public void MarkAsFound()
    {
        _wasFound = true;
    }
    public void OnSeenByPlayer()
    {
        if (_wasFound) return;

        _currentState?.OnSeenByPlayer(this);
    }

    public void StartHiding()
    {
        if (_currentState is ChildAwaitingAppearanceState || _currentState is ChildHidingState) return;
        Debug.Log("[ChildEnemy] El niño se esconde.");
        _wasFound = false;
        SwitchState(new ChildAwaitingAppearanceState());
    }
}
