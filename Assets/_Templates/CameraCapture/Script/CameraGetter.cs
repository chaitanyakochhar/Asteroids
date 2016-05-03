using UnityEngine;
using System;
using UnityEngine.UI;

public class CameraGetter : MonoBehaviour
{
    public string key;
    public bool isWebcamPicture = true;
    public void Start()
    {
        Texture2D t = null;
        bool test = false;
        if(Application.platform == RuntimePlatform.IPhonePlayer && isWebcamPicture)
        {
            transform.Rotate(new Vector3(0, 0, 180));
        }

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
}

