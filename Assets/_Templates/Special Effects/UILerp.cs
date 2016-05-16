using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UILerp : Effect
{
    public GameObject finalLocation;
    public AudioClip clip;
    public float duration = 1f;
    public bool START_EFFECT_ON_PLAY = false;

    public void Start()
    {
        if(START_EFFECT_ON_PLAY)
        {
            StartEffect();
        }
    }

    public override void StartEffect()
    {
        print("Start effect");
        StartCoroutine(EffectCoroutine());
    }

    public override IEnumerator EffectCoroutine()
    {
        float startTime = Time.time;
        Vector3 startPos = GetComponent<RectTransform>().anchoredPosition3D;
        if (clip != null)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
        }
        while ((Time.time - startTime) <= duration)
        {
            GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(startPos, finalLocation.GetComponent<RectTransform>().anchoredPosition3D, (Time.time - startTime) / duration);
            yield return null;
        }
        yield return null;
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }
}
