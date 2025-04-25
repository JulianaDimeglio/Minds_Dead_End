using Game.Mediators.Implementations;
using Game.Mediators.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyMediator _enemyMediator;
    [SerializeField] private float _huntAgainIn = 20f;
    // Start is called before the first frame update
    void Start()
    {
        //_enemyMediator.NotifyChildFound();
    }

    // Update is called once per frame
    void Update()
    {
        _huntAgainIn -= Time.deltaTime;
        if (_huntAgainIn <= 0)
        {
            _enemyMediator.NotifyChildFound();
            _huntAgainIn = 20f;
        }
    }
}
