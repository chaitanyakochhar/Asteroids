using UnityEngine;
using System.Collections;

public class DecisionTracker : MonoBehaviour
{

    public string Decision;
    public bool PushOnStart = false;
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
        if(PushOnStart)
        {
            OnClickHandler();
        }
    }
    public void OnClickHandler()
    {
        if (p != null && !ClickedOnce)
        {
            p.AddDecision(Decision);
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
}
