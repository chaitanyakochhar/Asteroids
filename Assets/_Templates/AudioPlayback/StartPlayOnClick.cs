using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class StartPlayOnClick : Effect
{
    public float delay;
    public AudioClip clip;

    public override IEnumerator EffectCoroutine()
    {
        yield return new WaitForSeconds(delay);
        if(clip!=null)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }
}
