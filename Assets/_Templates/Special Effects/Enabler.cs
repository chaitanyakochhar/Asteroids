using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Enabler : MonoBehaviour
{
    public GameObject[] objectsToCheck;
    public GameObject[] objectsToEnable;
    public bool disableObjectsToEnable = true;
    public bool PlayEffects = true;
    private List<Authenticator> checkAuthenticators;

    public void Start()
    {
        Authenticator a;
        checkAuthenticators = new List<Authenticator>();
        foreach (GameObject GO in objectsToCheck)
        {
            a = null;
            a = GO.GetComponent<Authenticator>();
            if (a != null)
            {
                checkAuthenticators.Add(a);
            }
        }
        if (checkAuthenticators.Count > 0 && disableObjectsToEnable)
        {
            foreach (GameObject GO in objectsToEnable)
            {
                if (GO.GetComponent<Button>() != null)
                {
                    GO.GetComponent<Button>().interactable = false;
                }
                else
                {
                    GO.SetActive(false);
                }
            }
        }

        StartCoroutine(pollForActivation());
    }

    private IEnumerator pollForActivation()
    {
        bool result;
        while (true)
        {
            result = true;
            foreach (Authenticator a in checkAuthenticators)
            {
                result &= a.isAuthenticated;
                yield return null;
            }

            if (result)
            {
                foreach (GameObject GO in objectsToEnable)
                {
                    if (GO.GetComponent<Button>() != null)
                    {
                        GO.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        GO.SetActive(true);
                    }
                    Effect[] effects = GO.GetComponents<Effect>();
                    if (PlayEffects)
                    {
                        foreach(Effect e in effects)
                        {
                            e.StartEffect();
                        }
                    }
                    yield return null;
                }
                break;
            }
            yield return null;
        }
        yield return null;

    }
}
