using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : Effect
{
    public string key;
    private PlayerDataManager p;

    public override void StartEffect()
    {
        if (GameObject.Find("Data Manager") != null)
        {
            p = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
            if (p != null)
            {
                AudioClip clip;
                bool check = p.GetData<AudioClip>(p.audioCaptured, key, out clip);
                if (check)
                {
                    GetComponent<AudioSource>().PlayOneShot(clip);
                }
            }
        }
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return null;
    }

    public override void StopEffect()
    {
        ;
    }
}
