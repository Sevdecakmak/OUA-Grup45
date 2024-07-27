using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lights = new List<Light>();
    [SerializeField] private TransitionBlockerController transitionBlockerController;

    private bool isOpen = false;
    private float minIntensity = 1.5f;
    private float maxIntensity = 3.0f;
    private float flickerDuration = 0.1f;

    private void Start()
    {
        //başlangıçta ışıkları kapalı yap
        foreach (Light light in lights)
        {
            light.intensity = 0;
        }

        transitionBlockerController.SetBlockersActive(true);
    }

    public void Interact()
    {
        ToggleLights();
    }

    private void ToggleLights()
    {
        isOpen = !isOpen;
        float targetIntensity = isOpen ? maxIntensity : 0;

        foreach (Light light in lights)
        {
            light.DOIntensity(targetIntensity, 1).OnComplete(() =>
            {
                if (isOpen)
                {
                    StartCoroutine(FlickerLight(light));
                    StartCoroutine(AutoTurnOffLights());
                }
            });
        }

        transitionBlockerController.SetBlockersActive(!isOpen);
    }

    private IEnumerator FlickerLight(Light light)
    {
        while (isOpen)
        {
            float randomIntensity = Random.Range(minIntensity, maxIntensity);
            light.DOIntensity(randomIntensity, flickerDuration);
            yield return new WaitForSeconds(flickerDuration);
        }
    }

    private IEnumerator AutoTurnOffLights()
    {
        yield return new WaitForSeconds(4);
        if (isOpen)
        {
            ToggleLights();
        }
    }
}
