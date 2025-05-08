using UnityEngine;

public class ObjetoRevertidor : MonoBehaviour, IInteraction
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float _timerObstacleBase = 5f;
    [SerializeField] private float _timerObstacle;
    [SerializeField] private bool _invertedControl;
    private bool _isInteracting = false;  

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
        _isInteracting = true;
    }

    //Este metodo invierte los controles y el jugador va hacia adelante al presionar S y viceversa.
    //El metodo cuenta con un timer ajustable, en el momento que se invierten los controles, el timer se dispara.
    private void InvertController()
    {
        if (_isInteracting)
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


    //Este metodo revierte los controles a su estado default.
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
                _isInteracting = false;
            }
        }
    }


}
