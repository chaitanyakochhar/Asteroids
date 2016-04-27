using UnityEngine;
using System.Collections;

public class SaveDrawing : MonoBehaviour
{
    public GameObject DrawingSurface;
    public string key;
    private PlayerDataManager p;

    public void Start()
    {
        if (GameObject.Find("Data Manager") != null)
        {
            p = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
        }
    }

    public void OnClickListener()
    {
        if (DrawingSurface != null)
        {
            Texture2D t = DrawingSurface.GetComponent<Renderer>().material.mainTexture as Texture2D;
            if (p != null)
            {
                p.AddData<Texture2D>(p.imagesCaptured, key, t);
            }
        }

    }


}
