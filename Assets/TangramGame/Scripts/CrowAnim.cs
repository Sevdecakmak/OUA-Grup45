using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAnim : MonoBehaviour
{
    public float breathSpeed = 1f;
    public float breathCycleTime = 2f;
    public Color blackColor = Color.black;
    public Color whiteColor = Color.white;

    private SpriteRenderer[] spriteRenderers;
    private bool isBreathingIn = true;
    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(Breathe());
    }
    private IEnumerator Breathe()
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < breathSpeed)
            {
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    if (isBreathingIn)
                    {
                        spriteRenderer.color = Color.Lerp(blackColor, whiteColor, elapsedTime / breathSpeed);
                    }
                    else
                    {
                        spriteRenderer.color = Color.Lerp(whiteColor, blackColor, elapsedTime / breathSpeed);
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isBreathingIn = !isBreathingIn;
        }
    }
/*
    private IEnumerator Breathe()
    {
        while (true)
        {

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                if (isBreathingIn)
                {
                    // Beyaza doğru nefes alma
                    spriteRenderer.color = Color.Lerp(spriteRenderer.color, whiteColor, Time.deltaTime * breathSpeed);
                }
                else
                {
                    // Siyaha doğru nefes verme
                    spriteRenderer.color = Color.Lerp(spriteRenderer.color, blackColor, Time.deltaTime * breathSpeed);
                }
            }

            if (isBreathingIn)
            {
                if (spriteRenderers[0].color == whiteColor)
                {
                    isBreathingIn = false;
                }
            }
            else
            {
                if (spriteRenderers[0].color == blackColor)
                {
                    isBreathingIn = true;
                }
            }

            yield return null;
        }
    }
    */
}
