using UnityEngine;
using System.Collections;

public class DebugButton : MonoBehaviour
{
    public void AssertPass()
    {
        GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().result = true;
        GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().LoadSceneFromURL(GameNames.ExitPage);
    }
    public void AssertFail()
    {
        GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().result = false;
        GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().LoadSceneFromURL(GameNames.ExitPage);
    }

}
