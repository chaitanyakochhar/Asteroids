using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DecisionTracker : Effect
{

    public string EventName;
    public SerializableDictionary MixPanelParameters;
    public bool PushOnStart = false;
    [Tooltip("These objects have a boolean as a Toggle or Authenticator")]
    public KeyObjectPair[] stateOfObjects;

    private PlayerDataManager p;

    public void Start()
    {
        if (GameObject.Find("Data Manager") != null)
        {
            p = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
        }
        else
        {
            print("Data Manager null");
        }

        if (PushOnStart)
        {
            StartEffect();
        }
    }

    public override void StartEffect()
    {
        AddMetaData();
        AddBoolData();
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        if (p != null)
        {
            ;
        }
    }

    public override IEnumerator EffectCoroutine()
    {
        if (Mixpanel.Token.Length <= 0)
            yield return new WaitForSeconds(1);
        if (p != null)
        {
            if (EventName.Length > 0)
            {
                p.PushToMixPanel(EventName, MixPanelParameters.getDictionary());
            }
        }
        yield return null;
    }

    private void AddMetaData()
    {
        MixPanelParameters.Add("Platform", Application.platform.ToString());
        MixPanelParameters.Add("Is Mobile Platform", Application.isMobilePlatform.ToString());
        MixPanelParameters.Add("Resolution", Screen.width + "x" + Screen.height);
    }

    private void AddBoolData()
    {
        foreach (KeyObjectPair KO in stateOfObjects)
        {
            if (KO.Object.GetComponent<Toggle>() != null)
            {
                MixPanelParameters.Add(KO.Key, KO.Object.GetComponent<Toggle>().isOn.ToString());
            }
            if (KO.Object.GetComponent<Authenticator>() != null)
            {
                MixPanelParameters.Add(KO.Key, KO.Object.GetComponent<Authenticator>().isAuthenticated.ToString());
            }
        }
    }
}


