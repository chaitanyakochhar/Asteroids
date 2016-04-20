using UnityEngine;
using System;

public class CameraGetter : MonoBehaviour
{
    public string key;
    public void Start()
    {
        Texture2D t;
        bool test = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>().imagesCaptured.TryGetValue(key, out t);
        if (test)
        {
            GetComponent<Renderer>().material.mainTexture = t;
            t.Apply();
        }
    }
}

