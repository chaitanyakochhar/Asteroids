using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIBounce : Effect
{
    public bool START_EFFECT_ON_PLAY = false;
    public float waitTime = 2f;
    public float bounceDuration = 0.3f;
    public int bounceCount = 5;
    public Vector3 bounceAmount = new Vector3(1.2f, 1.2f, 1f);
    private Vector3 startScale;

    private float startTime;

    public override IEnumerator EffectCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i<bounceCount; i++)
            {
                startTime = Time.time;
                while((Time.time - startTime) <= bounceDuration)
                {
                    GetComponent<Image>().rectTransform.localScale = Vector3.Lerp(startScale, bounceAmount, (Time.time - startTime) / bounceDuration);
                    yield return null;
                }
                startTime = Time.time;
                while ((Time.time - startTime) <= bounceDuration)
                {
                    GetComponent<Image>().rectTransform.localScale = Vector3.Lerp(bounceAmount, startScale, (Time.time - startTime) / bounceDuration);
                    yield return null;
                }
            }
            yield return null;
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
    // Use this for initialization
    void Start()
    {
        startScale = GetComponent<Image>().rectTransform.localScale;
        if (START_EFFECT_ON_PLAY)
        {
            StartEffect();
        }
    }

}
