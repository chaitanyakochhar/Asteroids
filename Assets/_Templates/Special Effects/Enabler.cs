using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enabler : MonoBehaviour
{
    public GameObject[] objectsToCheck;
    public GameObject[] objectsToEnable;

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
        if (checkAuthenticators.Count > 0)
        {
            foreach (GameObject GO in objectsToEnable)
            {
                GO.SetActive(false);
            }
            StartCoroutine(pollForActivation());

        }
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
                    GO.SetActive(true);
                    yield return null;
                }
                break;
            }
            yield return null;
        }
        yield return null;

    }
}
