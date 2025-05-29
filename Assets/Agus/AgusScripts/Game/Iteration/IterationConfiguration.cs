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

    private void ConfigureIteration(int iteration, LoopContextProvider provider)
    {
        LoopManager.Instance.SetConditionMet(false);
        ILoopState state = IterationFactory.GetStateForIteration(iteration, provider);
        // define a reference to the previous state
        if(iteration > 1)
        {
            ILoopState previousState = IterationFactory.GetStateForIteration(iteration - 1, provider);
            // execute the method backToNormal of previous state
            previousState.CleanIteration();
        }
        // execute the method backToNormal of previous state

        state.Configure();
    }
}