using UnityEngine;
using System.Collections;

public class UIJump : Effect
{
    public Vector3 displacement;
    public AudioClip clip;
    public float duration = 1f;
    public bool PLAY_EFFECT_ON_START = false;

    private float newDuration;

    public void Start()
    {
        if(PLAY_EFFECT_ON_START)
        {
            StartEffect();
        }
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override IEnumerator EffectCoroutine()
    {
        float startTime = Time.time;
        Vector3 startPos = GetComponent<RectTransform>().anchoredPosition3D;
        Vector3 endPos = GetComponent<RectTransform>().anchoredPosition3D + displacement;
        while (true)
        {
            
            if (clip != null)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
            }
            startTime = Time.time;
            newDuration = Random.Range(duration, duration + 1);
            while ((Time.time - startTime) <= newDuration)
            {
                GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / newDuration);
                yield return null;
            }
            startTime = Time.time;
            newDuration = Random.Range(duration, duration + 1);
            while ((Time.time - startTime) <= newDuration)
            {
                GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(endPos, startPos, (Time.time - startTime) / newDuration);
                yield return null;
            }
            yield return null;
        }
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }
}
