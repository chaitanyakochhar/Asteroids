using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class WordHighlighter : Effect
{
    public HighlightWordInfo[] words;
    public GameObject buttonToSpawn;

    private List<GameObject> objectsSpawned;

    public void Start()
    {
        StartEffect();
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return new WaitForEndOfFrame();
        Text t = GetComponent<Text>();
        int[] wordStartLocations;
        string[] splitText = getWords(t.text, out wordStartLocations);
        for (int i = 0; i < splitText.Length; i++)
        {
            foreach (HighlightWordInfo info in words)
            {
                if (info.Text.ToLower() == splitText[i].ToLower())
                {
                    spawnButton(t, info, wordStartLocations[i], t.cachedTextGenerator.GetCharactersArray());
                }
            }
        }
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        ;
    }

    private string[] getWords(string text, out int[] wordStartLocations)
    {
        char[] separators = { ' ', '.', '/', ':', ';', ',' };
        List<int> separatorLocations = new List<int>();
        separatorLocations.Add(0);
        for (int i = text.IndexOf(' '); i > -1; i = text.IndexOf(' ', i + 1))
        {
            // for loop end when i=-1 ('a' not found)
            separatorLocations.Add(i + 1);
        }

        string[] splitText = text.Split(separators);
        wordStartLocations = separatorLocations.ToArray();
        return splitText;
    }

    private void spawnButton(Text t, HighlightWordInfo info, int wordStartLocation, UICharInfo[] charInfo)
    {
        Vector2 location = t.rectTransform.position;
        location += charInfo[wordStartLocation].cursorPos;
        location += new Vector2(0, -t.fontSize / 2);
        float totalLength = 0;
        for (int i = wordStartLocation; i<wordStartLocation+info.Text.Length; i++)
        {
            totalLength += charInfo[i].charWidth;
        }
        location += new Vector2(totalLength / 2, 0);
        GameObject GO = Instantiate(buttonToSpawn, location, Quaternion.identity) as GameObject;
        GO.transform.parent = transform.parent;
        GO.GetComponent<Button>().image.rectTransform.sizeDelta = new Vector2(totalLength,t.fontSize);
        GO.transform.position = location;
    }
}
