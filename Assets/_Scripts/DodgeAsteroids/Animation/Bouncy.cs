using UnityEngine;
using System.Collections;
using System;

public class Bouncy : Effect
{
    public bool PLAY_EFFECT_ON_START = false;
    public bool DESTROY_ON_COMPLETE = false;
    public float animationDuration = 2f;
    public Vector3 normal = new Vector3(1, 1, 1);
    public Vector3 large = new Vector3(2, 2, 1);

    private float startTime;

    private IEnumerator GrowShrinkAnimation()
    {
        while(true)
        {
            startTime = Time.time;
            while((Time.time - startTime) <= animationDuration / 2)
            {
                transform.localScale = Vector3.Lerp(normal, large, (Time.time - startTime) / (animationDuration / 2));
                yield return null;
            }
            startTime = Time.time;
            while ((Time.time - startTime) <= animationDuration / 2)
            {
                transform.localScale = Vector3.Lerp(large, normal, (Time.time - startTime) / (animationDuration / 2));
                yield return null;
            }
            if(DESTROY_ON_COMPLETE)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    public void StartBouncy()
    {
        StartCoroutine(GrowShrinkAnimation());
    }

    public override void StartEffect()
    {
        StartBouncy();
    }

    public override void StopEffect()
    {
        transform.localScale = normal;
        StopAllCoroutines();
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return null;
    }

    public void Start()
    {
        if(PLAY_EFFECT_ON_START)
        {
            StartEffect();
        }
    }
}
