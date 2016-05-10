using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animation))]
public class AnimateOnClick : Effect
{
    public bool START_ON_PLAY = false;

    public override IEnumerator EffectCoroutine()
    {
        GetComponent<Animation>().Play();
        yield return new WaitForSeconds(2);
        GetComponent<Animation>().Stop();
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
        if (START_ON_PLAY)
        {
            StartEffect();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        print("Boom!");
    }
}
