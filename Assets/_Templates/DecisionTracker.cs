using UnityEngine;
using System.Collections;
using System;

public class DecisionTracker : Effect
{

    public string Decision;
    public bool PushOnStart = false;
    public bool PushToMixpanel = false;
    private PlayerDataManager p;
    private bool ClickedOnce = false;

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
            OnClickHandler();
        }
    }
    public void OnClickHandler()
    {
        if (p != null && !ClickedOnce)
        {
            p.AddDecision(Decision);
            if (PushToMixpanel)
                p.PushToMixPanel(Decision);
            ClickedOnce = true;
        }
        else
        {
            print("PlayerDataManager script is not present on Data Manager");
        }
    }
    public void AddDecision()
    {
        p.AddDecision(Decision);
    }

    public override void StartEffect()
    {
        OnClickHandler();
    }

    public override void StopEffect()
    {
        ;
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return null;
    }
}
