using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float bobSpeed = 10f;
    public float bobSpeedSprint = 15f;
    public float bobAmount = 0.05f;
    public PlayerMovement player;

    private float timer = 0f;
    private Vector3 startPosition;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        startPosition = transform.localPosition;
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        Vector3 playerMovement = player.transform.position - lastPlayerPosition;
        float speed = playerMovement.magnitude / Time.deltaTime;
        bool sprint = player.isSprinting;

        if (speed > 0.1f && !sprint)
        {
            timer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(timer) * bobAmount;
            transform.localPosition = startPosition + new Vector3(0, bobOffset, 0);
        }
        else if (speed > 0.1f && sprint)
        {
            timer += Time.deltaTime * bobSpeedSprint;
            float bobOffset = Mathf.Sin(timer) * bobAmount;
            transform.localPosition = startPosition + new Vector3(0, bobOffset, 0);
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * bobSpeed);
        }

        lastPlayerPosition = player.transform.position;
    }
}
