using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class CameraGetter : Effect
{
    public string key;
    public bool isWebcamPicture = true;
    public void Start()
    {

        if(Application.platform == RuntimePlatform.IPhonePlayer && isWebcamPicture)
        {
            transform.Rotate(new Vector3(0, 0, 180));
        }

        StartEffect();
    }

    public override void StartEffect()
    {
        Texture2D t = null;
        bool test = false;

        if (GameObject.Find("Data Manager") != null)
            test = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>().imagesCaptured.TryGetValue(key, out t);
        if (test)
        {
            if (GetComponent<RectTransform>() == null)
                GetComponent<Renderer>().material.mainTexture = t;
            else
                GetComponent<RawImage>().texture = t;
            t.Apply();
        }
    }

    public override void StopEffect()
    {
        ;
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return null;
    }
}

