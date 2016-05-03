using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIBlink : Effect
{
    public bool START_EFFECT_ON_PLAY = false;
    public Color startColor = Color.white;
    public Color endColor = Color.clear;
    public float blinkDuration = 0.5f;
    public int blinkCount = 3;
    public float waitBeforeStart = 10f;

    public override IEnumerator EffectCoroutine()
    {
        float startTime;
        blinkDuration *= 2f;
        while (true)
        {

            yield return new WaitForSeconds(waitBeforeStart);
            for (int i = 0; i < blinkCount; i++)
            {
                startTime = Time.time;   
                while ((Time.time - startTime) <= blinkDuration)
                {
                    GetComponent<Image>().color = Color.Lerp(startColor, endColor, (Time.time - startTime) / blinkDuration);
                    yield return null;
                }
                startTime = Time.time;
                while ((Time.time - startTime) <= blinkDuration)
                {
                    GetComponent<Image>().color = Color.Lerp(endColor, startColor, (Time.time - startTime) / blinkDuration);
                    yield return null;
                }
            }
            yield return null;
        }
    }

    public void Start()
    {
        if(START_EFFECT_ON_PLAY)
        {
            StartEffect();
        }
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }
}
