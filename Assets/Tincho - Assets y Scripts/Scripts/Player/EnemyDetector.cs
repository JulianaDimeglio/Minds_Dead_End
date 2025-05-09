using UnityEngine;
using System.Collections.Generic;

public class EnemyDetector : MonoBehaviour
{
    [Header("Configuración de detección")]
    [SerializeField] private float sphereCastRadius = 0.6f;
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float checkInterval = 0.3f;
    [SerializeField] private LayerMask detectionLayer;

    [Header("Puntos por enemigo (para detectar partes visibles)")]
    [SerializeField] private int samplePointCount = 5;
    [SerializeField] private float sampleOffset = 0.3f;

    private float _nextCheckTime;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        if (_camera == null)
            Debug.LogError("[PlayerVisionSystem] No se encontró la cámara principal.");
    }

    private void Update()
    {
        if (Time.time >= _nextCheckTime)
        {
            _nextCheckTime = Time.time + checkInterval;
            DetectVisibleEnemies();
        }
    }

    private void DetectVisibleEnemies()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastRadius, detectionRange, detectionLayer);

        foreach (var hit in hits)
        {
            var detectable = hit.collider.GetComponent<IDetectableByPlayer>();
            if (detectable == null) continue;

            Collider enemyCollider = hit.collider;

            if (!IsInCameraFrustum(enemyCollider.bounds.center)) continue;
            Debug.Log(HasAnyVisiblePoint(enemyCollider) ? "[EnemyDetector] Enemy detected!" : "[EnemyDetector] Enemy not detected!");
            if (HasAnyVisiblePoint(enemyCollider))
            {
                detectable.OnSeenByPlayer();
            }
        }
    }

    private bool IsInCameraFrustum(Vector3 worldPoint)
    {
        Vector3 vp = _camera.WorldToViewportPoint(worldPoint);
        return vp.z > 0 && vp.x > 0 && vp.x < 1 && vp.y > 0 && vp.y < 1;
    }

    private bool HasAnyVisiblePoint(Collider collider)
    {
        List<Vector3> points = GetSamplePoints(collider, samplePointCount);
        var targetDetectable = collider.GetComponentInParent<IDetectableByPlayer>();

        foreach (var point in points)
        {
            if (!IsInCameraFrustum(point)) continue;
            Debug.Log("dadada");
            if (!Physics.Linecast(_camera.transform.position, point))
            {
                return true;
            }
        }

        return false;
    }

    private List<Vector3> GetSamplePoints(Collider collider, int count)
    {
        var bounds = collider.bounds;
        var points = new List<Vector3>();

        points.Add(bounds.center); // punto central

        if (count <= 1) return points;

        // puntos alrededor del centro
        points.Add(bounds.center + Vector3.up * sampleOffset);
        points.Add(bounds.center - Vector3.up * sampleOffset);
        points.Add(bounds.center + Vector3.right * sampleOffset);
        points.Add(bounds.center - Vector3.right * sampleOffset);

        // si querés más, podés agregar diagonales u otros ejes

        return points;
    }

    private void OnDrawGizmosSelected()
    {
        if (_camera == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_camera.transform.position + _camera.transform.forward * detectionRange, sphereCastRadius);
        Gizmos.DrawRay(_camera.transform.position, _camera.transform.forward * detectionRange);
    }
}