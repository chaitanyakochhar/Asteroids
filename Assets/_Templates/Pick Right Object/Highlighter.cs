using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Listener))]
public class Highlighter : MonoBehaviour
{

    public GameObject[] spriteSurfaces;
    public float highlightDuration = 1f;
    public Color offColor = Color.black;

    private Listener listenerScript;
    private PickObjectPhase[] phases;

    // Use this for initialization
    void Start()
    {
        listenerScript = GetComponent<Listener>();
        phases = listenerScript.phases;
    }

    public void HighlightPhase(int phaseIndex)
    {
        StartCoroutine(HighlightCoroutine(phaseIndex));
    }

    private IEnumerator HighlightCoroutine(int phaseIndex)
    {
        yield return new WaitForSeconds(1);
        int surfaceToHighlight;
        float startTime;
        if(phaseIndex<=phases.Length)
        for (int i = 0; i < phases[phaseIndex].correctObjects.Length; i++)
        {

            surfaceToHighlight = i % spriteSurfaces.Length;
            startTime = Time.time;
            while ((Time.time - startTime) <= highlightDuration)
            {
                spriteSurfaces[surfaceToHighlight].GetComponent<Image>().color = Color.Lerp(offColor, phases[phaseIndex].correctObjects[i].GetComponent<PickableObject>().iconColor, (Time.time - startTime) / highlightDuration);
                yield return null;
            }

            startTime = Time.time;
            while ((Time.time - startTime) <= highlightDuration)
            {
                spriteSurfaces[surfaceToHighlight].GetComponent<Image>().color = Color.Lerp(phases[phaseIndex].correctObjects[i].GetComponent<PickableObject>().iconColor, offColor, (Time.time - startTime) / highlightDuration);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
