using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    private float timer = 0f;
    private Vector3 startPosition;
    private Vector3 lastPlayerPosition;

    [Header("Bobbing Parameters")]
    public float bobSpeedWalk;
    public float bobSpeedSprint;
    public float bobAmountWalkSprint;
    public float bobAmountIdle;
    public float bobSpeedIdle;
    public PlayerMovement player;

    void Start()
    {
        startPosition = transform.localPosition;
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        VerticalHeadBobbing();
    }

    private void VerticalHeadBobbing ()
    {
        Vector3 playerMovement = player.transform.position - lastPlayerPosition;
        float speed = playerMovement.magnitude / Time.deltaTime;
        bool sprint = player.isSprinting;

        //Walk bobbing
        if (speed > 0.1f && !sprint)
        {
            timer += Time.deltaTime * bobSpeedWalk;
            float bobOffset = Mathf.Sin(timer) * bobAmountWalkSprint;
            transform.localPosition = startPosition + new Vector3(0, bobOffset, 0);
        }

        //Sprint bobbing
        else if (speed > 0.1f && sprint)
        {
            timer += Time.deltaTime * bobSpeedSprint;
            float bobOffset = Mathf.Sin(timer) * bobAmountWalkSprint;
            transform.localPosition = startPosition + new Vector3(0, bobOffset, 0);
        }

        //Idle bobbing
        else if (speed == 0)
        {

            //timer = 0;
            //transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * bobSpeed);
            timer += Time.deltaTime * bobSpeedIdle;
            float bobOffset = Mathf.Sin(timer) * bobAmountIdle;
            transform.localPosition = startPosition + new Vector3(0, bobOffset, 0);
        }

        lastPlayerPosition = player.transform.position;
    }
}
