using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class StartPlayWithDelay : Effect
{

    public AudioClip clip;
    public float waitTime = 1f;
    public GameObject[] stopTheseSounds;

    public void Start()
    {
        StartEffect();
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<AudioSource>().PlayOneShot(clip);
        if (stopTheseSounds.Length > 0)
            foreach (GameObject GO in stopTheseSounds)
            {
                if (GO != null && GO.GetComponent<AudioSource>() != null)
                {
                    GO.GetComponent<AudioSource>().Stop();
                }
            }
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
