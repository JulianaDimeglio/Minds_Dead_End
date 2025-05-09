using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Transform target; // El jugador
    [SerializeField] private float distance = 5f; // Distancia normal de la c�mara
    [SerializeField] private float minDistance = 1f; // Distancia m�nima a la que puede acercarse
    [SerializeField] private float smoothSpeed = 10f; // Suavizado del movimiento
    [SerializeField] private LayerMask collisionLayers; // Las capas que bloquean la c�mara

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position - target.forward * distance;
        RaycastHit hit;

        if (Physics.SphereCast(target.position, 0.3f, -target.forward, out hit, distance, collisionLayers))
        {
            // Si choca, colocamos la c�mara en el punto de impacto
            float hitDistance = Mathf.Max(hit.distance - 0.3f, minDistance);
            desiredPosition = target.position - target.forward * hitDistance;
        }

        // Movemos la c�mara suavemente
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);

        // Opcional: miramos siempre al jugador
        transform.LookAt(target);
    }
}