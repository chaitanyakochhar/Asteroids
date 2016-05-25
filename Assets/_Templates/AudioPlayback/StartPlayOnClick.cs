using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class StartPlayOnClick : Effect
{
    public float delay;
    public AudioClip clip;
    public GameObject[] stopTheseAudioSources;

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

        foreach (GameObject GO in stopTheseAudioSources)
        {
            if (GO != null && GO.GetComponent<AudioSource>() != null)
            {
                GO.GetComponent<AudioSource>().Stop();
            }
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
