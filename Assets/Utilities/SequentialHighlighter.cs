using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SequentialHighlighter : MonoBehaviour
{
    public AudioClip[] playingSounds;
    public GameObject[] Objects;

    private AudioSource source;

    public Color dimColor = new Color(1, 1, 1, 0.2f);
    public Color normalColor = new Color(1, 1, 1, 1);

    public void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(startHighlight());
    }

    private IEnumerator startHighlight()
    {

        foreach(GameObject GO in Objects)
        {
            SpriteRenderer s = GO.GetComponent<SpriteRenderer>();
            if (s != null)
            {
                s.color = dimColor;
            }
            yield return null;
        }

        for(int i = 0; i<Objects.Length; i++)
        {
            SpriteRenderer s = Objects[i].GetComponent<SpriteRenderer>();
            if(s!=null)
            {
                s.color = normalColor;
            }
            if (i < playingSounds.Length)
            {
                source.PlayOneShot(playingSounds[i]);
                yield return new WaitForSeconds(playingSounds[i].length);
            }
            yield return new WaitForSeconds(2f);
            s.color = dimColor;
            yield return null;
        }

        foreach (GameObject GO in Objects)
        {
            SpriteRenderer s = GO.GetComponent<SpriteRenderer>();
            if (s != null)
            {
                s.color = normalColor;
            }
            yield return null;
        }

        yield return null;


    }
}
