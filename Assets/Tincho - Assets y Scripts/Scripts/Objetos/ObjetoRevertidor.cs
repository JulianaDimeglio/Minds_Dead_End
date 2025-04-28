using UnityEngine;

public class ObjetoRevertidor : MonoBehaviour, IInteraction
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float _timerObstacleBase = 5f;
    [SerializeField] private float _timerObstacle;
    [SerializeField] private bool _invertedControl;
    public bool isInteracting = false;  

    private void Start()
    {
        _timerObstacle = _timerObstacleBase;

        _invertedControl = false;
    }

    private void Update()
    {
        InvertController();
        RevertController();
    }

    public void TriggerInteraction()
    {
        isInteracting = true;
    }

    private void InvertController()
    {
        if (isInteracting)
        {
            if (!_invertedControl)
            {
                _invertedControl = true;
                player.InvertZAxis(true);
                print("Obstaculo activado!");
                _timerObstacle = _timerObstacleBase;
            }
        }
    }

    private void RevertController()
    {
        if (_invertedControl)
        {
            _timerObstacle -= Time.deltaTime;

            if (_timerObstacle <= 0)
            {
                player.InvertZAxis(false);
                _invertedControl = false;
                print("Controles restaurados.");
                isInteracting = false;
            }
        }
    }


}
