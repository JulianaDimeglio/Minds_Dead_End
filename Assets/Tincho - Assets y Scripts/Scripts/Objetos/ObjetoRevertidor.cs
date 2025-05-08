using UnityEngine;

public class ObjetoRevertidor : MonoBehaviour
{
    private PlayerMovement player;
    private Interact playerState;
    [SerializeField] private float _timerObstacleBase = 5f;
    [SerializeField] private float _timerObstacle;
    [SerializeField] private bool _invertedControl;

    private void Start()
    {
        _timerObstacle = _timerObstacleBase;

        _invertedControl = false;
    }

    private void Update()
    {
        ChangeControllersObstacle();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovement>();
            playerState = other.GetComponent<Interact>();

            if (player != null && playerState.hasInteractered)
            {
                //Debug.Log("Jugador detectado");

                if (!_invertedControl)
                {
                    _invertedControl = true;
                    player.zAxisDirection *= -1;
                    print("Obstaculo activado!");
                    _timerObstacle = _timerObstacleBase;
                }
            }
        }
    }

    private void ChangeControllersObstacle()
    {
        if (_invertedControl)
        {
            _timerObstacle -= Time.deltaTime;

            if (_timerObstacle <= 0)
            {
                player.zAxisDirection *= -1;
                _invertedControl = false;
                //print("Controles restaurados.");
            }
        }
    }


}
