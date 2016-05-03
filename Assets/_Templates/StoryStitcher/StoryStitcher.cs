using UnityEngine;
using System.Collections.Generic;

public class StoryStitcher : MonoBehaviour
{
    private PlayerDataManager p;
    private List<string> decisions;
    private GameObject current;
    private Vector3 position;
    private int i = 0;

    public void Start()
    {
        if (GameObject.Find("Data Manager") != null)
        {
            p = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
            decisions = p.decisions;
            i = FindNextScene(true);
            LoadPage(i);
            
        }
    }

    public void Next()
    {
        i = FindNextScene(true);
        print(i);
        LoadPage(i);
    }

    public void Previous()
    {
        i = FindNextScene(false);
        print(i);
        LoadPage(i);
    }
    
    private int FindNextScene(bool forward)
    {

        GameObject current = null;
        while(current == null)
        {
            if (forward)
            {
                i = (i + 1) % decisions.Count;
            }
            else
            {
                if (i <= 0)
                {
                    i = decisions.Count - 1;
                }
                else i--;
            }
            current = GameObject.Find(p.decisions[i]);
            if(current!=null)
            {
                break;
            }
        }
        return i;
    }
    
    private void LoadPage(int index)
    {
        if(current!=null)
        {
            current.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            current.transform.position = position;
        }
        current = null;
        current = GameObject.Find(p.decisions[i]);
        if(current!=null)
        {
            position = current.transform.position;
            current.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            current.GetComponent<Canvas>().worldCamera = Camera.main;
            current.GetComponent<Canvas>().sortingOrder = 0;
            foreach(Transform child in current.transform)
            {
                if(child.GetComponent<AudioPlayer>()!=null)
                {
                    child.GetComponent<AudioPlayer>().StartEffect();
                }
            }
        

        }
    }    
}
