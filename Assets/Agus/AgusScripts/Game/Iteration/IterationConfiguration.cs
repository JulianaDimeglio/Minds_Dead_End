using UnityEngine;

public class IterationConfigurator : MonoBehaviour
{
    private void OnEnable()
    {
        if (LoopManager.Instance != null)
            LoopManager.Instance.OnLoopChanged += ConfigureIteration;
    }

    private void OnDisable()
    {
        if (LoopManager.Instance != null)
            LoopManager.Instance.OnLoopChanged -= ConfigureIteration;
    }

    private void ConfigureIteration(int iteration)
    {
        LoopManager.Instance.SetConditionMet(false);
        ILoopState state = IterationFactory.GetStateForIteration(iteration);
        state.Configure();
    }
}