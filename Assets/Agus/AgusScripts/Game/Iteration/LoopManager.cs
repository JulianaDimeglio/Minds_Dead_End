using System;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance { get; private set; }

    public int CurrentIteration { get; private set; } = 1;
    public event Action<int> OnLoopChanged;

    private bool _conditionMet = false;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetConditionMet(bool value) => _conditionMet = value;

    public void TryAdvanceLoop()
    {
        if (_conditionMet)
        {
            CurrentIteration++;
        }

        _conditionMet = false;
        OnLoopChanged?.Invoke(CurrentIteration);
    }
}