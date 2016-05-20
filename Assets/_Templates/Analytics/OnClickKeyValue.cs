using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DecisionTracker))]
public class OnClickKeyValue : MonoBehaviour
{
    public string Key;
    public void OnClickValue(string s)
    {
        GetComponent<DecisionTracker>().MixPanelParameters.Add(Key, s);
    }
    public void OnClickValue(bool b)
    {
        GetComponent<DecisionTracker>().MixPanelParameters.Add(Key, b.ToString());
    }
}
