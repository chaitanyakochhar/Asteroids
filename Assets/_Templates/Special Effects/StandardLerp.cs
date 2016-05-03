using UnityEngine;
using System.Collections;
using System;

public class StandardLerp : Effect
{
    public GameObject destination;
    public float duration;
    public bool PLAY_ON_START = false;

    public void Start()
    {
        if(PLAY_ON_START)
        {
            StartCoroutine(EffectCoroutine());
        }
    }

    public override IEnumerator EffectCoroutine()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        while((Time.time - startTime)<=duration)
        {
            transform.position = Vector3.Lerp(startPosition, destination.transform.position, (Time.time - startTime) / duration);
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
}
