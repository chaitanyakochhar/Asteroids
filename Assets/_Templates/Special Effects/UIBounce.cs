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
    public Vector2 bounceAmount = new Vector3(1.2f, 1.2f, 1f);

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
                    GetComponent<Image>().rectTransform.localScale = Vector3.Lerp(Vector3.one, bounceAmount, (Time.time - startTime) / bounceDuration);
                    yield return null;
                }
                startTime = Time.time;
                while ((Time.time - startTime) <= bounceDuration)
                {
                    GetComponent<Image>().rectTransform.localScale = Vector3.Lerp(bounceAmount, Vector3.one, (Time.time - startTime) / bounceDuration);
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
    public void StopEffect()
    {
        StopAllCoroutines();
    }    
    // Use this for initialization
    void Start()
    {
        if (START_EFFECT_ON_PLAY)
        {
            StartEffect();
        }
    }

}
