using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Authenticator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Highlighter))]
public class Listener : MonoBehaviour
{
    public bool EvaluateInSequence = false;
    public AudioClip passSound;
    public AudioClip failSound;
    public PickObjectPhase[] phases;

    private List<PickableObject> objectsPicked;
    private int currentPhase = 0;

    public void Start()
    {
        objectsPicked = new List<PickableObject>();
        Input.multiTouchEnabled = true;
        GetComponent<Highlighter>().HighlightPhase(currentPhase);
    }

    public void RegisterObject(GameObject callingObject)
    {
        if (callingObject.GetComponent<PickableObject>() != null && currentPhase < phases.Length)
        {
            objectsPicked.Add(callingObject.GetComponent<PickableObject>());
            if (objectsPicked.Count == phases[currentPhase].correctObjects.Length)
            {
                bool result = Evaluate(currentPhase);
                if (result)
                {
                    GetComponent<AudioSource>().PlayOneShot(passSound);
                    currentPhase++;
                }
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(failSound);
                }
                objectsPicked.Clear();
            }
        }

        if (currentPhase >= phases.Length)
        {
            GetComponent<Authenticator>().isAuthenticated = true;
        }


    }
    public void DeregisterObject(GameObject callingObject)
    {
        if (callingObject.GetComponent<PickableObject>() != null)
        {
            objectsPicked.Remove(callingObject.GetComponent<PickableObject>());
        }
    }

    private bool Evaluate(int phaseIndex)
    {
        if (EvaluateInSequence)
        {
            for (int i = 0; i < phases[phaseIndex].correctObjects.Length; i++)
            {
                if (objectsPicked[i].gameObject != phases[phaseIndex].correctObjects[i])
                {
                    return false;
                }
            }
        }
        else
        {

            List<GameObject> correctObjs = new List<GameObject>(phases[phaseIndex].correctObjects);
            for (int i = 0; i < phases[phaseIndex].correctObjects.Length; i++)
            {
                if (!correctObjs.Contains(objectsPicked[i].gameObject))
                {
                    return false;
                }
                else
                {
                    correctObjs.Remove(objectsPicked[i].gameObject);
                }
            }
            if(correctObjs.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;

    }
}
