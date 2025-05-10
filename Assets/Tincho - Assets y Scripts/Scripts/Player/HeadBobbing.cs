using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    //This class handles how camera will bob, imitating a breathing effect on screen.
    private float timer = 0f;
    private Vector3 startPosition;
    private Vector3 lastPlayerPosition;

    [Header("Bobbing Parameters")]
    [SerializeField] private float bobSpeedWalk;
    [SerializeField] private float bobSpeedSprint;
    [SerializeField] private float bobAmountWalkSprint;
    [SerializeField] private float bobAmountIdle;
    [SerializeField] private float bobSpeedIdle;
    [SerializeField] private PlayerMovement player;

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
            timer += Time.deltaTime * bobSpeedIdle;
            float bobOffset = Mathf.Sin(timer) * bobAmountIdle;
            transform.localPosition = startPosition + new Vector3(0, bobOffset, 0);
        }

        lastPlayerPosition = player.transform.position;
    }
}
