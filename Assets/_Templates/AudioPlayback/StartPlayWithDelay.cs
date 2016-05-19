using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class StartPlayWithDelay : Effect
{

    public AudioClip clip;
    public float waitTime = 1f;

    public void Start()
    {
        StartEffect();
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<AudioSource>().PlayOneShot(clip);
        yield return null;
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        GetComponent<AudioSource>().Stop();
    }
}
