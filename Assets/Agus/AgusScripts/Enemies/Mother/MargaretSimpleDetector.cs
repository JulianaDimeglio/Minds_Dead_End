using UnityEngine;

public class MargaretSimpleDetector : MonoBehaviour
{
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private LayerMask detectionLayer;

    private Camera _camera;
    private bool _triggered = false;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_triggered) return;

        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionDistance, detectionLayer))
        {
            if (hit.collider.CompareTag("Margaret"))
            {
                _triggered = true;

                // Buscar el componente y llamar al evento
                var scare = hit.collider.GetComponent<MargaretScareTrigger>();
                if (scare != null)
                {
                    scare.TriggerScare();
                }
            }
        }
    }
}