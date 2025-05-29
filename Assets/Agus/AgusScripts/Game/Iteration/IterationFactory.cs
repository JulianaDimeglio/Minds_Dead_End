using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public static class IterationFactory
{
    public static ILoopState GetStateForIteration(int iteration, LoopContextProvider provider)
    {

        UnityEngine.Debug.Log("IterationFactory: GetStateForIteration called with iteration: " + iteration);
        
        switch (iteration)
        {
            case 1: return new FirstIterationState(provider.firstIterationContext?.ToPlain());
            case 2: return new SecondIterationState(provider.secondIterationContext?.ToPlain());
            case 3: return new ThirdIterationState(provider.thirdIterationContext?.ToPlain());
            default: return new FirstIterationState(provider.firstIterationContext?.ToPlain());
        }
    }
}