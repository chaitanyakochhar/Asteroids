using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class FindLocationOfText : MonoBehaviour
{
    Text t;
    public GameObject spawnSprite;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator test()
    {
        yield return new WaitForEndOfFrame();
        t = GetComponent<Text>();
        RectTransform textLocation = t.rectTransform;
        UICharInfo[] info = t.cachedTextGenerator.GetCharactersArray();
        UIVertex[] v = t.cachedTextGenerator.GetVerticesArray();
        Vector2 offset = t.rectTransform.position;

        GameObject first = Instantiate(spawnSprite, info[0].cursorPos, Quaternion.identity) as GameObject;
        first.transform.SetParent(transform.parent);
        Vector2 boomFirst = new Vector2(info[0].charWidth / 2, -t.fontSize / 2);
        first.transform.position = (info[0].cursorPos + offset + boomFirst);

        for (int i = 1; i<info.Length; i++)
        {
            

            if(t.text[i-1]==' ')
            {
                GameObject GO = Instantiate(spawnSprite, info[i].cursorPos, Quaternion.identity) as GameObject;
                GO.transform.SetParent(transform.parent);
                GO.GetComponent<Button>().image.rectTransform.sizeDelta = new Vector2(info[i].charWidth, t.fontSize);
                Vector2 boom = new Vector2(info[i].charWidth/2, -t.fontSize/2);
                GO.transform.position = (info[i].cursorPos + offset + boom);
            }
        }
        //foreach(UICharInfo x in info)
        //{
        //    GameObject GO = Instantiate(spawnSprite, x.cursorPos, Quaternion.identity) as GameObject;
        //    GO.transform.SetParent(transform.parent);
        //    GO.transform.position = (x.cursorPos + offset);
        //}
    }
}
