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
            if(GetComponent<UIBounce>()!=null)
            {
                GetComponent<UIBounce>().StopEffect();
            }
        }
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }
}
