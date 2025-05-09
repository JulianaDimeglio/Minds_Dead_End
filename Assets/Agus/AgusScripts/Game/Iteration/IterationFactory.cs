
using UnityEngine.SceneManagement;

public static class IterationFactory
{
    public static ILoopState GetStateForIteration(int iteration)
    {
        switch (iteration)
        {
            case 1: return new FirstIterationState();
            case 2: return new ShadowIterationState();
            case 3: return new ChildIterationState();
            default: return new FirstIterationState();
        }
    }
}