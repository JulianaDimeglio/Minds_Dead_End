using UnityEngine;

public class InverterObject : MonoBehaviour, IInteraction
{
    // This class handles the InverterObject behavior. By the time user interacts with it, user's zAxis controls will get inverted.

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

    //This method has an adjustable timer, by the time controls get inverted, timer gets triggered.
    private void InvertController()
    {
        if (_isInteracting)
        {
            if (!_invertedControl)
            {
                _invertedControl = true;
                player.InvertZAxis(true);
                print("Obstacle activated!");
                _timerObstacle = _timerObstacleBase;
            }
        }
    }

    // This method set the controls back to default state.
    private void RevertController()
    {
        if (_invertedControl)
        {
            _timerObstacle -= Time.deltaTime;

            if (_timerObstacle <= 0)
            {
                player.InvertZAxis(false);
                _invertedControl = false;
                print("Controls reset.");
                _isInteracting = false;
            }
        }
    }


}
