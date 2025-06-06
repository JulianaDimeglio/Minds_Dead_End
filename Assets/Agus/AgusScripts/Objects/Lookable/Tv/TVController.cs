using System.Collections;
using UnityEngine;

public class TVController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TVVideoController videoController;
    [SerializeField] private MeshRenderer tvScreenRenderer;
    [SerializeField] private Light[] tvLights;

    [Header("Materials")]
    [SerializeField] private Material screenOnMaterial;
    [SerializeField] private Material screenOffMaterial;

    private bool isOn = false;

    private void Start()
    {
        TurnOff();
    }

    public void TurnOn()
    {
        if (isOn) return;
        isOn = true;

        foreach (var light in tvLights)
            light.enabled = true;

        tvScreenRenderer.material = screenOnMaterial;

        videoController.PlayStaticVideo();
    }

    public void TurnOff()
    {
        if (!isOn) return;
        isOn = false;

        foreach (var light in tvLights)
            light.enabled = false;

        tvScreenRenderer.material = screenOffMaterial;

        videoController.StopVideoPlayback();
    }

    public void TurnOnDelayed(float delay = 1f)
    {
        StartCoroutine(TurnOnCoroutine(delay));
    }

    private IEnumerator TurnOnCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        TurnOn();
    }
}