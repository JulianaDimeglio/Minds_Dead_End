using UnityEngine;

public class FogStarter : MonoBehaviour
{
    public float simulatedTime = 10f;

    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Simulate(simulatedTime, true, true);
            ps.Play();
        }
    }
}